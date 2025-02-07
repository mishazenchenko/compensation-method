<<<<<<< HEAD
#include "waveform.h"

/* эта функция определяет уровни вершины (top), основания (base), амплитуду (amp), уровни 10 % амплитуды (lev10pc), 90 % амплитуды (lev90pc),
 * точки входа и выхода для областей гистерезиса при определении длительностей положительной и отрицательной частей импульса, длительностей
 * фронта и среза. Для определения уровней и типа сигнала рассчитывается гистограмма. Так, для определения, является ли сигнал постоянным
 * или импульсным, проверяются первые STATES столбцы гистограммы. Если они пусты, сигнал считается постоянным, измеряются только максимальное
 * и минимальное значения. */
Bundle Waveform::GetSignalParams(short* wave,unsigned long length,unsigned long userSkew=0)
{
    constexpr int histSize{(MAX-MIN)/STEP+1};
    unsigned long hist[histSize]{};
    unsigned long maxCount[STATES]{};
    int           histColumns[STATES]{};

    bool skip;
    int top,base,middle,lev10pc,lev90pc;
    unsigned long enterPositiveIndex,exitPositiveIndex,enterNegativeIndex,exitNegativeIndex,anotherEnterIndex,anotherExitIndex,
                  enter10up,exit10up,enter90up,exit90up,enter90down,exit90down,enter10down,exit10down;
    std::vector<unsigned long> enterValues(3);
    std::vector<unsigned long> exitValues(3);
    Bundle bundle;

    for(unsigned long i{0};i!=length;++i)
    {
        if(wave[i]==HOLE) continue;
        hist[(wave[i]-MIN)/STEP]++;
    }

    for(short j{0};j!=STATES;++j)
    {
        for(int i{0};i!=histSize;++i)
        {
            for(short t{0};t!=STATES;++t)
            {
                if(i==histColumns[t])
                {
                    skip=true;
                    break;
                }
            }
            if(skip)
            {
                skip=false;
                continue;
            }
            if(hist[i]>=maxCount[j])
            {
                maxCount[j]=hist[i];
                histColumns[j]=i;
            }
        }
    }

    bundle.sT=SignalType::pulse;

    for(short i{0};i!=STATES;++i)
    {
        if(maxCount[i]==0)
        {
            bundle.sT=SignalType::dc;
            FindMaxMin(wave,0,length-1,0,0,bundle.minTopLevel,bundle.maxTopLevel);
            return bundle;
        }
    }

    if(histColumns[0]<histColumns[1]) Swap(histColumns[0],histColumns[1]);
    top= histColumns[0];
    base=histColumns[1];

    Conversion(top, STEP,MIN);
    Conversion(base,STEP,MIN);

    middle=(top+base)/2;
    lev90pc=0.9*top+0.1*base;
    lev10pc=0.1*top+0.9*base;

    Hysteresis(wave,STEP,middle,StepType::rise,0,length-1,enterPositiveIndex,exitPositiveIndex);
    Hysteresis(wave,STEP,middle,StepType::fall,0,length-1,enterNegativeIndex,exitNegativeIndex);

    /* предполагается, что на экране осциллографа отображается более одного полного периода сигнала. В противном случае
     * выбросится исключение, измерения проведены не будут */
    if(enterPositiveIndex<enterNegativeIndex)  //для случая, когда первый перепад положительный
    {
        Hysteresis(wave,STEP,middle,StepType::rise,exitNegativeIndex,length-1,anotherEnterIndex,anotherExitIndex);

        enterValues[0]=enterPositiveIndex;
        enterValues[1]=enterNegativeIndex;
        enterValues[2]=anotherEnterIndex;

        exitValues[0]=exitPositiveIndex;
        exitValues[1]=exitNegativeIndex;
        exitValues[2]=anotherExitIndex;

        Swap(enterPositiveIndex,anotherEnterIndex,exitPositiveIndex,anotherExitIndex);
    }
    else                                        //для случая, когда первый перепад отрицательный
    {
        Hysteresis(wave,STEP,middle,StepType::fall,exitPositiveIndex,length-1,anotherEnterIndex,anotherExitIndex);

        enterValues[0]=anotherExitIndex;
        enterValues[1]=exitPositiveIndex;
        enterValues[2]=exitNegativeIndex;

        exitValues[0]=anotherEnterIndex;
        exitValues[1]=enterPositiveIndex;
        exitValues[2]=enterNegativeIndex;

        Swap(enterNegativeIndex,anotherEnterIndex,exitNegativeIndex,anotherExitIndex);
    }

    /* если первый перепад отрицательный, волновая форма как бы отображается относительно вертикальной оси и рассматривается от момента
     * последнего перепада, который отрицательный, в обратную сторону */
    bundle.widthPositive=MeanDif(enterValues[0],exitValues[0],enterValues[1],exitValues[1]);
    bundle.widthNegative=MeanDif(enterValues[1],exitValues[1],enterValues[2],exitValues[2]);

    Hysteresis(wave,STEP,lev10pc,StepType::fall,enterPositiveIndex,0,exit10up,enter10up);
    Hysteresis(wave,STEP,lev90pc,StepType::rise,exitPositiveIndex,length-1,enter90up,exit90up);
    Hysteresis(wave,STEP,lev90pc,StepType::rise,enterNegativeIndex,0,exit90down,enter90down);
    Hysteresis(wave,STEP,lev10pc,StepType::fall,exitNegativeIndex,length-1,enter10down,exit10down);

    bundle.riseTime=MeanDif(enter10up,exit10up,enter90up,exit90up);
    bundle.fallTime=MeanDif(enter90down,exit90down,enter10down,exit10down);

    FindMaxMin(wave,exitValues[0],enterValues[1],(userSkew==0)?5*bundle.riseTime:userSkew,0.1*bundle.widthPositive,
               bundle.minTopLevel,bundle.maxTopLevel);
    FindMaxMin(wave,exitValues[1],enterValues[2],(userSkew==0)?5*bundle.fallTime:userSkew,0.1*bundle.widthNegative,
               bundle.minBaseLevel,bundle.maxBaseLevel);

    return bundle;
}

/* расчет моментов входа и выхода в/из области гистерезиса. hystStep определяет половину области гистерезиса, level - центр
 * области гистерезиса, d - возрастающий или убывающий сигнал рассматривается, initIndex - начальный индекс values[],
 * lastIndex - конечный индекс values[], enterIndex - полученный индекс входа в область гистерезиса, exitIndex - полученный
 * индекс выхода из области гистерезиса. lastIndex может быть меньше initIndex, нужно иметь это в виду при выборе d */
void Waveform::Hysteresis(short values[],int hystStep,int level,StepType d,unsigned long initIndex,
                unsigned long lastIndex,unsigned long& enterIndex,unsigned long& exitIndex)
{
    unsigned long initStored{initIndex};
    bool inWindow{true};

    enterIndex=initStored,
    exitIndex= initStored;

    for(const short incr=Cmpr(lastIndex,initIndex);initIndex!=lastIndex+incr;initIndex+=incr)
    {
        if(values[initIndex]>=(level-hystStep)&&values[initIndex]<=(level+hystStep))
        {
            if(!inWindow)
            {
                enterIndex=initIndex;
                inWindow=true;
            }
            else
            {
                continue;
            }
        }
        else
        {
            if(inWindow&&
               enterIndex!=initStored&&
               Cmpr(values[enterIndex-1],level)!=Cmpr(values[initIndex],level)&&
               Cmpr(values[initIndex],level)==(int)d)
            {
                exitIndex=initIndex;
                return;
            }
            inWindow=false;
        }
    }
    throw MyException("Не найдены точки входа и выхода из области гистерезиса!");
}

//расчет максимального и минимального значений волновой формы на интервале [initIndex,lastIndex] с учетом сдвигов leftSkew, rightSkew
void Waveform::FindMaxMin(short values[],unsigned long initIndex,unsigned long lastIndex,
                unsigned long leftSkew,unsigned long rightSkew,short& minValue,short& maxValue)
{
    const short incr=Cmpr(lastIndex,initIndex);
    if(Cmpr((lastIndex-incr*rightSkew),(initIndex+incr*leftSkew))!=incr)
    {
        throw MyException("Неправильно заданы индексы при поиске максимального и минимального значений волновой формы");
    }
    maxValue=MIN;
    minValue=MAX;
    initIndex+=incr*leftSkew;
    for(;initIndex!=lastIndex-incr*rightSkew+incr;initIndex+=incr)
    {
        if(values[initIndex]==HOLE) continue;
        if(values[initIndex]>maxValue) maxValue=values[initIndex];
        if(values[initIndex]<minValue) minValue=values[initIndex];
    }
}
=======
#include "waveform.h"

/* эта функция определяет уровни вершины (top), основания (base), амплитуду (amp), уровни 10 % амплитуды (lev10pc), 90 % амплитуды (lev90pc),
 * точки входа и выхода для областей гистерезиса при определении длительностей положительной и отрицательной частей импульса, длительностей
 * фронта и среза. Для определения уровней и типа сигнала рассчитывается гистограмма. Так, для определения, является ли сигнал постоянным
 * или импульсным, проверяются первые STATES столбцы гистограммы. Если они пусты, сигнал считается постоянным, измеряются только максимальное
 * и минимальное значения. */
Bundle Waveform::GetSignalParams(short* wave,unsigned long length,unsigned long userSkew=0)
{
    constexpr int histSize{(MAX-MIN)/STEP+1};
    unsigned long hist[histSize]{};
    unsigned long maxCount[STATES]{};
    int           histColumns[STATES]{};

    bool skip;
    int top,base,middle,lev10pc,lev90pc;
    unsigned long enterPositiveIndex,exitPositiveIndex,enterNegativeIndex,exitNegativeIndex,anotherEnterIndex,anotherExitIndex,
                  enter10up,exit10up,enter90up,exit90up,enter90down,exit90down,enter10down,exit10down;
    std::vector<unsigned long> enterValues(3);
    std::vector<unsigned long> exitValues(3);
    Bundle bundle;

    for(unsigned long i{0};i!=length;++i)
    {
        if(wave[i]==HOLE) continue;
        hist[(wave[i]-MIN)/STEP]++;
    }

    for(short j{0};j!=STATES;++j)
    {
        for(int i{0};i!=histSize;++i)
        {
            for(short t{0};t!=STATES;++t)
            {
                if(i==histColumns[t])
                {
                    skip=true;
                    break;
                }
            }
            if(skip)
            {
                skip=false;
                continue;
            }
            if(hist[i]>=maxCount[j])
            {
                maxCount[j]=hist[i];
                histColumns[j]=i;
            }
        }
    }

    bundle.sT=SignalType::pulse;

    for(short i{0};i!=STATES;++i)
    {
        if(maxCount[i]==0)
        {
            bundle.sT=SignalType::dc;
            FindMaxMin(wave,0,length-1,0,0,bundle.minTopLevel,bundle.maxTopLevel);
            return bundle;
        }
    }

    if(histColumns[0]<histColumns[1]) Swap(histColumns[0],histColumns[1]);
    top= histColumns[0];
    base=histColumns[1];

    Conversion(top, STEP,MIN);
    Conversion(base,STEP,MIN);

    middle=(top+base)/2;
    lev90pc=0.9*top+0.1*base;
    lev10pc=0.1*top+0.9*base;

    Hysteresis(wave,STEP,middle,StepType::rise,0,length-1,enterPositiveIndex,exitPositiveIndex);
    Hysteresis(wave,STEP,middle,StepType::fall,0,length-1,enterNegativeIndex,exitNegativeIndex);

    /* предполагается, что на экране осциллографа отображается более одного полного периода сигнала. В противном случае
     * выбросится исключение, измерения проведены не будут */
    if(enterPositiveIndex<enterNegativeIndex)  //для случая, когда первый перепад положительный
    {
        Hysteresis(wave,STEP,middle,StepType::rise,exitNegativeIndex,length-1,anotherEnterIndex,anotherExitIndex);

        enterValues[0]=enterPositiveIndex;
        enterValues[1]=enterNegativeIndex;
        enterValues[2]=anotherEnterIndex;

        exitValues[0]=exitPositiveIndex;
        exitValues[1]=exitNegativeIndex;
        exitValues[2]=anotherExitIndex;

        Swap(enterPositiveIndex,anotherEnterIndex,exitPositiveIndex,anotherExitIndex);
    }
    else                                        //для случая, когда первый перепад отрицательный
    {
        Hysteresis(wave,STEP,middle,StepType::fall,exitPositiveIndex,length-1,anotherEnterIndex,anotherExitIndex);

        enterValues[0]=anotherExitIndex;
        enterValues[1]=exitPositiveIndex;
        enterValues[2]=exitNegativeIndex;

        exitValues[0]=anotherEnterIndex;
        exitValues[1]=enterPositiveIndex;
        exitValues[2]=enterNegativeIndex;

        Swap(enterNegativeIndex,anotherEnterIndex,exitNegativeIndex,anotherExitIndex);
    }

    /* если первый перепад отрицательный, волновая форма как бы отображается относительно вертикальной оси и рассматривается от момента
     * последнего перепада, который отрицательный, в обратную сторону */
    bundle.widthPositive=MeanDif(enterValues[0],exitValues[0],enterValues[1],exitValues[1]);
    bundle.widthNegative=MeanDif(enterValues[1],exitValues[1],enterValues[2],exitValues[2]);

    Hysteresis(wave,STEP,lev10pc,StepType::fall,enterPositiveIndex,0,exit10up,enter10up);
    Hysteresis(wave,STEP,lev90pc,StepType::rise,exitPositiveIndex,length-1,enter90up,exit90up);
    Hysteresis(wave,STEP,lev90pc,StepType::rise,enterNegativeIndex,0,exit90down,enter90down);
    Hysteresis(wave,STEP,lev10pc,StepType::fall,exitNegativeIndex,length-1,enter10down,exit10down);

    bundle.riseTime=MeanDif(enter10up,exit10up,enter90up,exit90up);
    bundle.fallTime=MeanDif(enter90down,exit90down,enter10down,exit10down);

    FindMaxMin(wave,exitValues[0],enterValues[1],(userSkew==0)?5*bundle.riseTime:userSkew,0.1*bundle.widthPositive,
               bundle.minTopLevel,bundle.maxTopLevel);
    FindMaxMin(wave,exitValues[1],enterValues[2],(userSkew==0)?5*bundle.fallTime:userSkew,0.1*bundle.widthNegative,
               bundle.minBaseLevel,bundle.maxBaseLevel);

    return bundle;
}

/* расчет моментов входа и выхода в/из области гистерезиса. hystStep определяет половину области гистерезиса, level - центр
 * области гистерезиса, d - возрастающий или убывающий сигнал рассматривается, initIndex - начальный индекс values[],
 * lastIndex - конечный индекс values[], enterIndex - полученный индекс входа в область гистерезиса, exitIndex - полученный
 * индекс выхода из области гистерезиса. lastIndex может быть меньше initIndex, нужно иметь это в виду при выборе d */
void Waveform::Hysteresis(short values[],int hystStep,int level,StepType d,unsigned long initIndex,
                unsigned long lastIndex,unsigned long& enterIndex,unsigned long& exitIndex)
{
    unsigned long initStored{initIndex};
    bool inWindow{true};

    enterIndex=initStored,
    exitIndex= initStored;

    for(const short incr=Cmpr(lastIndex,initIndex);initIndex!=lastIndex+incr;initIndex+=incr)
    {
        if(values[initIndex]>=(level-hystStep)&&values[initIndex]<=(level+hystStep))
        {
            if(!inWindow)
            {
                enterIndex=initIndex;
                inWindow=true;
            }
            else
            {
                continue;
            }
        }
        else
        {
            if(inWindow&&
               enterIndex!=initStored&&
               Cmpr(values[enterIndex-1],level)!=Cmpr(values[initIndex],level)&&
               Cmpr(values[initIndex],level)==(int)d)
            {
                exitIndex=initIndex;
                return;
            }
            inWindow=false;
        }
    }
    throw MyException("Не найдены точки входа и выхода из области гистерезиса!");
}

//расчет максимального и минимального значений волновой формы на интервале [initIndex,lastIndex] с учетом сдвигов leftSkew, rightSkew
void Waveform::FindMaxMin(short values[],unsigned long initIndex,unsigned long lastIndex,
                unsigned long leftSkew,unsigned long rightSkew,short& minValue,short& maxValue)
{
    const short incr=Cmpr(lastIndex,initIndex);
    if(Cmpr((lastIndex-incr*rightSkew),(initIndex+incr*leftSkew))!=incr)
    {
        throw MyException("Неправильно заданы индексы при поиске максимального и минимального значений волновой формы");
    }
    maxValue=MIN;
    minValue=MAX;
    initIndex+=incr*leftSkew;
    for(;initIndex!=lastIndex-incr*rightSkew+incr;initIndex+=incr)
    {
        if(values[initIndex]==HOLE) continue;
        if(values[initIndex]>maxValue) maxValue=values[initIndex];
        if(values[initIndex]<minValue) minValue=values[initIndex];
    }
}
>>>>>>> 368ab02 (bash commit)
