#include "compensationmethod.h"
#include "waveform.h"

//деструктор
CompensationMethod::~CompensationMethod()
{
    if(valuesOscTop)       delete[] valuesOscTop;
    if(valuesOscBase)      delete[] valuesOscBase;
    if(calcTop)            delete[] calcTop;
    if(calcBase)           delete[] calcBase;
    if(wave)               delete[] wave;
    if(timeMarksTop)       delete[] timeMarksTop;
    if(timeMarksBase)      delete[] timeMarksBase;
}

//вычисление среднего значения массива значений
template<typename T>
T CompensationMethod::MeanValue(T* values,unsigned long length)
{
    if(!values) throw MyException("Отсутствует массив для расчета среднего значения!");

    T mean{0};

    for (unsigned long i{0};i!=length;++i)
    {
        mean+=values[i];
    }

    mean/=length;

    return mean;
}

//функция получения волновой формы и ее параметров (начальных значений и приращений развертки и отклонения)
void CompensationMethod::GetWave(const char channel)
{
    if(wave) {delete[] wave; wave=nullptr;}

    vc.osc.WriteCommand((std::string)":WAVeform:SOURce CHANnel"+channel);

//    vc.osc.Query(":ACQuire:POINts?");                                       dataPoints= atol(vc.osc.buffer);
    vc.osc.Query(":WAVEform:YINCrement?");                                  yIncrement= atof(vc.osc.buffer);
    vc.osc.Query(":WAVEform:YORigin?");                                     yOrigin=    atof(vc.osc.buffer);
    vc.osc.Query(":WAVEform:XINCrement?");                                  xIncrement= atof(vc.osc.buffer);
    vc.osc.Query(":WAVEform:XORigin?");                                     xOrigin=    atof(vc.osc.buffer);

    dataPoints=MAX_WAVE_POINTS;
    wave=new short[MAX_WAVE_POINTS];
    vc.GetWave(wave,dataPoints);
}

//ПРЕДУПРЕЖДЕНИЕ: ОЧЕНЬ ХРУПКИЙ КОД!!!
/* вычисление средних на интервалах значений с помощью полученной волновой формы и заданной пользователем длительности интервала,
 * результатом работы являются массив с временнЫми метками и массив со средними значениями, также длина массивов */
void CompensationMethod::GetValues(double*& values,QString*& timeMarks,unsigned long& length)
{
    if(!wave)                   throw MyException("Волновая форма не получена (измерение средних интервальных значений)!");
    if(timeMarks) {delete[] timeMarks;  timeMarks=nullptr;}
    if(values)    {delete[] values;     values=nullptr;}

    if(dataPoints<2)            throw MyException("Недостаточно точек волновой формы (измерение средних интервальных значений)!");
    if(xIncrement<=0)           throw MyException("Некорректное значение шага дискретизации волновой формы (измерение средних интервальных значений)!");
    if(intervalTime<xIncrement) throw MyException("Некорректное значение интервала времени (измерение средних интервальных значений)!");

    length = std::ceil(xIncrement*(dataPoints-1)/intervalTime);

    values=new double[length]{};
    timeMarks=new QString[length];

    for (unsigned long i0{0},i1{0},previousTemp{0},count{0},temp;i1!=dataPoints;++i1)
    {
        temp=i1*xIncrement/intervalTime;
        if(temp==length) --temp;
        values[temp]+=wave[i1]*yIncrement+yOrigin;

        if(temp==previousTemp)
        {
            ++count;
        }
        else
        {
            values[previousTemp]/=count;
            timeMarks[previousTemp] = QString("%1 -- %2 нс").arg((xIncrement*i0+xOrigin)*1000000000,0,'f',3)
                                                            .arg((xIncrement*(i1-1)+xOrigin)*1000000000,0,'f',3);

            i0=i1;
            previousTemp=temp;
            count=1;
        }

        if(i1==dataPoints-1)
        {
            values[temp]/=count;
            timeMarks[temp] = QString("%1 -- %2 нс").arg((xIncrement*i0+xOrigin)*1000000000,0,'f',3)
                                                    .arg((xIncrement*i1+xOrigin)*1000000000,0,'f',3);
        }
    }
}

//ПРЕДУПРЕЖДЕНИЕ: ОЧЕНЬ ХРУПКИЙ КОД!!!
/* вычисление мгновенных значений на границах интервалов заданной длительности,
 * результатом работы являются массив с временнЫми метками и массив с мгновенными значениями, также длина массивов */
void CompensationMethod::GetValues2(double*& values,QString*& timeMarks,unsigned long& length)
{
    if(!wave)                   throw MyException("Волновая форма не получена (измерение мгновенных значений)!");
    if(timeMarks) {delete[] timeMarks; timeMarks=nullptr;}
    if(values)    {delete[] values;    values=nullptr;   }

    if(dataPoints==0)           throw MyException("Недостаточно точек волновой формы (измерение мгновенных значений)!");
    if(xIncrement<=0)           throw MyException("Некорректное значение шага дискретизации волновой формы (измерение мгновенных значений)!");
    if(intervalTime<xIncrement) throw MyException("Некорректное значение интервала времени (измерение мгновенных значений)!");

    length = std::ceil(xIncrement*(dataPoints-1)/intervalTime)+1;

    values=new double[length];
    timeMarks=new QString[length];

    for(unsigned int i{0},previousTemp{0},temp;i!=dataPoints;++i)
    {
        temp=i*xIncrement/intervalTime;

        if(temp!=previousTemp||i==0)
        {
            values[temp] = wave[i]*yIncrement+yOrigin;
            timeMarks[temp] = QString("%1 нс").arg((xIncrement*i+xOrigin)*1000000000,0,'f',3);
            previousTemp=temp;
        }

        if(i==dataPoints-1&&++temp!=length)
        {
            values[temp] = wave[i]*yIncrement+yOrigin;
            timeMarks[temp] = QString("%1 нс").arg((xIncrement*i+xOrigin)*1000000000,0,'f',3);
        }
    }
}

//установка коэффициента отлонения и смещения по вертикали в соответствии с максимальным и минимальным значениями сигнала
void CompensationMethod::SetVertical(double max,double min)
{
    double scaleVal;
    double value=Waveform::Mean(max,min);
    if(imp_50Ohm)
    {
        if(qFabs(value)>3.2)      scaleVal=0.27;
        else if(qFabs(value)>1.6) scaleVal=0.13;
        else if(qFabs(value)>0.8) scaleVal=0.056;
        else                      scaleVal=0.003;
    }
    else
    {
        if(qFabs(value)>20)       scaleVal=0.21;
        else if(pulse)            scaleVal=0.023;   //для импульсных сигналов, 1 МОм и положительных значений к.о. меньше 23 мВ/дел работают плохо
        else if(qFabs(value)>5)   scaleVal=0.01;
        else                      scaleVal=0.003;
    }
    if(scaleVal*8<(max-min))
    {
        vc.osc.WriteCommand(":CHANnel1:RANGe "+std::to_string(1.5*(max-min)));
        vc.osc.WriteCommand(":CHANnel2:RANGe "+std::to_string(1.5*(max-min)));
    }
    else
    {
        vc.osc.WriteCommand(":CHANnel1:SCALe "+std::to_string(scaleVal));
        vc.osc.WriteCommand(":CHANnel2:SCALe "+std::to_string(scaleVal));
    }
    vc.osc.WriteCommand(":CHANnel1:OFFSet "+std::to_string(value));
    vc.osc.WriteCommand(":CHANnel2:OFFSet "+std::to_string(value));
}

//задержка в секундах, можно передавать дробные значения в качестве аргумента
void CompensationMethod::Delay(double delay)
{
    QThread::msleep(delay*1000);
}

/* установка коэффициента разверки и смещения по горизонтали с помощью длительности интервала (time), значений задержки фронта/спада (leftSkew)
 * и длительности отбрасываемого участка (rightSkew); значение leftSkew может быть отрицательным, значение rightSkew отрицательным быть
 * не должно */
void CompensationMethod::SetTimebase(double time,double leftSkew=0,double rightSkew=0)
{
    std::ostringstream ostrstr;
    std::string s;
    if((time-qFabs(leftSkew)-rightSkew)<=0) throw MyException("Указано слишком большое значение задержки!");
    if(time!=0&&time!=9.99999E+37)
    {
        ostrstr<<(time-rightSkew-qFabs(leftSkew));
        vc.osc.WriteCommand(":TIMEbase:RANGe "+ostrstr.str());
    }
    ostrstr.str(std::string());
    ostrstr<<leftSkew;
    vc.osc.WriteCommand(":TIMEbase:POSition "+ostrstr.str());
}

//получить результат собственных измерений осциллографа для одного параметра
QString CompensationMethod::MeasRes()
{
    Delay(PRE_MEASURE_TIMEOUT);
    vc.osc.WriteCommand(":CDISplay");
    Delay(POST_MEASURE_TIMEOUT);
    vc.osc.Query(":MEASure:RESults?");
    return QString::fromUtf8(vc.osc.buffer);
}

//получить результат собственных измерений осциллографа для двух параметров
void CompensationMethod::MeasRes(double& res1,double& res2)
{
    bool flag1{false},
         flag2{false};

    Delay(PRE_MEASURE_TIMEOUT);
    vc.osc.WriteCommand(":CDISplay");
    Delay(POST_MEASURE_TIMEOUT);
    vc.osc.Query(":MEASure:RESults?");
    QString qsRes=QString::fromUtf8(vc.osc.buffer);
    QStringList qsl=qsRes.split(',');

    if(qsl.size()!=2) throw MyException("Недоступен результат пары параметров!");

    res1=qsl[0].toDouble(&flag1);
    res2=qsl[1].toDouble(&flag2);

    if(!(flag1&flag2)) throw MyException("Некорректный результат измерений пары параметров!");
}

/* расчет действительного значения напряжения (для Mode::average); standardU - установленное на калибраторе напряжение,
 * standardUosc - измеренное с помощью осциллографа напряжение калибратора, sourceUosc - измеренное напряжение источника */
double CompensationMethod::Calc(double standardU,double standardUosc,double sourceUosc)
{
    return sourceUosc-(standardUosc-standardU);
}

/* расчет массива действительных значений (для Mode::intervals и Mode::points); standardU - установленное на калибраторе напряжение,
 * standardUosc - измеренное с помощью осциллографа напряжение калибратора, sourceUosc - массив измеренных значений напряжения источника,
 * calcRes - массив действительных значений для каждого интервала, length - длины массивов sourceUosc и calcRes */
void CompensationMethod::Calc(double standardU,double standardUosc,double* sourceUosc,double*& calcRes,unsigned long length)
{
    if(!sourceUosc) throw MyException("Не передана для расчета волновая форма исследуемого сигнала");
    if(calcRes) {delete[] calcRes; calcRes=nullptr;}
    calcRes=new double[length];

    for(unsigned long i{0};i<length;i++)
    {
        calcRes[i] = sourceUosc[i]-(standardUosc-standardU);
    }
}

//реализация измерения межканальной погрешности
double CompensationMethod::InterChannelDeviation()
{
    double ch1Res1{0},
           ch1Res2{0},
           ch2Res1{0},
           ch2Res2{0};

    QMutex mutex;

    mutex.lock();

    SetTimebase(DC_TIMEBASE);
    vc.osc.WriteCommand(":TRIGger:SWEEP AUTO");
    emit message("Подключите формирователь эталона к каналу 1");
    qwc.wait(&mutex);
    if(!goFurther) return 0;
    vc.cal.WriteCommand("OUTPut:STATe ON");
    MeasRes(ch1Res1,ch1Res2);
    vc.cal.WriteCommand("OUTPut:STATe OFF");
    emit message("Подключите формирователь эталона к каналу 2");
    qwc.wait(&mutex);
    if(!goFurther) return 0;
    vc.cal.WriteCommand("OUTPut:STATe ON");
    MeasRes(ch2Res1,ch2Res2);
    vc.cal.WriteCommand("OUTPut:STATe OFF");

    mutex.unlock();
    return ch2Res1-ch1Res2;
}

//основная процедура
void CompensationMethod::MainProcedure()
{
    double pWidth,nWidth,maxTopLevel,minTopLevel,maxBaseLevel,minBaseLevel,riseTime;//,fallTime;
    std::string bits;
    Bundle bundle;
    QMutex mutex;

    mutex.lock();

    vc.osc.WriteCommand("*RST");                    //сброс
    vc.osc.WriteCommand(":SYSTem:HEADer OFF");      //настройки ответа на запрос волнвой формы
    vc.osc.WriteCommand(":WAVeform:FORMat WORD");

    vc.cal.WriteCommand("*RST");
    vc.cal.WriteCommand("ROUTe:SIGNal:PATH CH1");
    vc.cal.WriteCommand("SOURce:FUNCtion:SHAPe DC");

    if(!imp_50Ohm)
    {
        vc.osc.WriteCommand(":CHANnel1:INPut DC");      //сопротивление 1 МОм
        vc.osc.WriteCommand(":CHANnel2:INPut DC");
        vc.cal.WriteCommand("ROUTe:SIGNal:IMPedance 1E6");
    }
    else
    {
        vc.osc.WriteCommand(":CHANnel1:INPut DC50");    //сопротивление 50 Ом
        vc.osc.WriteCommand(":CHANnel2:INPut DC50");
        vc.cal.WriteCommand("ROUTe:SIGNal:IMPedance 50");
    }

    emit message("Подключите источник исследуемого сигнала к каналу 1 осциллографа");   //промпт
    qwc.wait(&mutex);

    if(!goFurther)      //прервать выполнение по решению пользователя
    {
        emit finished();
        return;
    }

    vc.osc.WriteCommand(":AUToscale"); /* автоматическая установка праметров отображения сигнала (к.о., к.р., и т.п.).
                                        * Иногда (особенно для постоянного напряжения) работает с задержкой, из-за чего
                                        * не удается получить волновую форму. Не знаю, в чем проблема */

    if(averaging)   //усредение заданного количества волновых форм
    {
        vc.osc.WriteCommand(":ACQuire:AVERage ON");
        vc.osc.WriteCommand(":ACQuire:COMPlete:STATe ON");        //ждать (ON) или нет (OFF) всех усредняемых волновых форм для измерений
        vc.osc.WriteCommand(":ACQuire:AVERage:COUNt "+std::to_string(averagingNum));
    }

    vc.osc.WriteCommand(":CHANnel2:DISPlay ON");                  //включить 2-ой канал
    vc.osc.WriteCommand(":MEASure:STATistics MEAN");

    Delay(POST_AUTOSCALE_TIMEOUT);
    GetWave('1');

    if(xIncrement==0) throw MyException("Значение периода дискретизации равно нулю!");
    bundle=Waveform::GetSignalParams(wave,dataPoints,skew/xIncrement);
    pulse=(bundle.sT==SignalType::pulse)? true:false;

    if(pulse)
    {
        vc.osc.WriteCommand(":TRIGger:SWEEP TRIG");
        vc.osc.WriteCommand(":TRIGger:EDGE:SOURce AUXiliary");

        /* Следующая команда задает уровень сигнала запуска. Предполагается, что запуск происходит при положительном перепаде.
         * Скорее всего, потребуется добавить возможность изменить уровень или тип перепада, а может и добавить временнОе смещение
         * исследуемого сигнала относительно сигнала запуска. В таком случае необходимо будет реализовать измерение
         * смещения, что можно сделать с помощью функции Waveform::GetSignalParams, получив доступ к переменным enterPositiveIndex,
         * exitPositiveIndex, enterNegativeIndex и т.д. Т.е.. псевдокод приблизительно следующий:
         * \включить второй канал, если он выключен;
         * \попросить оператора подать сигнал синхронизации на второй канал, а исследуемый сигнал на первый;
         * \используя Waveform::GetSignalParams измерить моменты времени фронта, соответствующие 50 % амплитуды обоих сигналов;
         * \разность полученных значений впоследсвтии как-то учесть (например, в качестве прибавки к CompensationMethod::skew)  */
        vc.osc.WriteCommand(":TRIGger:LEVel AUX,1");

        maxTopLevel=    bundle.maxTopLevel*yIncrement+yOrigin;
        minTopLevel=    bundle.minTopLevel*yIncrement+yOrigin;
        maxBaseLevel=   bundle.maxBaseLevel*yIncrement+yOrigin;
        minBaseLevel=   bundle.minBaseLevel*yIncrement+yOrigin;
        pWidth=         bundle.widthPositive*xIncrement;
        nWidth=         bundle.widthNegative*xIncrement;
        riseTime=       bundle.riseTime*xIncrement;
//        fallTime=       bundle.fallTime*xIncrement;
    }
    else
    {
        riseTime=0;
//        fallTime=0;
        maxTopLevel=bundle.maxTopLevel*yIncrement+yOrigin;
        minTopLevel=bundle.minTopLevel*yIncrement+yOrigin;
        pWidth=DC_TIMEBASE;
        nWidth=DC_TIMEBASE;
        skew=0;
    }

    SetTimebase(pWidth,(skew==0)?5*riseTime:skew,0.1*pWidth);
    vc.osc.WriteCommand(":TIMEbase:REFerence:PERCent 0");       //показывать положительную часть импульса

    switch(resolution)      //установка разерешения АЦП
    {
    case(Resolution::BITS10):
        bits="BITS10";
        break;
    case(Resolution::BITF11):
        bits="BITF11";
        break;
    case(Resolution::BITF12):
        bits="BITF12";
        break;
    case(Resolution::BITF13):
        bits="BITF13";
        break;
    case(Resolution::BITF14):
        bits="BITF14";
        break;
    case(Resolution::BITF15):
        bits="BITF15";
        break;
    case(Resolution::BITF16):
        bits="BITF16";
        break;
    }
    vc.osc.WriteCommand(":ACQuire:HRESolution "+bits);

    standardU_top=Waveform::Mean(maxTopLevel,minTopLevel);
    SetVertical(maxTopLevel,minTopLevel);

    vc.cal.WriteCommand("SOURce:VOLTage:LEVel:IMMediate:AMPLitude "+std::to_string(standardU_top));
    vc.cal.WriteCommand("OUTPut:STATe ON");
    vc.osc.WriteCommand(":MEASure:VAVerage DISPlay,CHANnel1");
    vc.osc.WriteCommand(":MEASure:VAVerage DISPlay,CHANnel2");

    MeasRes(standardUosc_top,sourceUosc_top);

    switch(mode)
    {
    case Mode::average:
        top = Calc(standardU_top,standardUosc_top,sourceUosc_top);

        emit setTopValue(top);

        vc.cal.WriteCommand("SOURce:VOLTage:LEVel:IMMediate:AMPLitude "+std::to_string(top));
        vc.osc.WriteCommand(":CDISplay");
        Delay(SHOW_OFF_TIME);

        break;
    case Mode::intervals:
    case Mode::points:
        GetWave('1');

        if(mode==Mode::intervals) GetValues(valuesOscTop,timeMarksTop,lengthTop);
        else                      GetValues2(valuesOscTop,timeMarksTop,lengthTop);

        Calc(standardU_top,standardUosc_top,valuesOscTop,calcTop,lengthTop);

        break;
    }

    vc.cal.WriteCommand("OUTPut:STATe OFF");

    if(pulse)
    {
        SetTimebase(nWidth,(skew==0)?-5*riseTime:-skew,0.1*nWidth);
        vc.osc.WriteCommand(":TIMEbase:REFerence:PERCent 100");     //показывать отрицательную часть импульса

        standardU_base=Waveform::Mean(maxBaseLevel,minBaseLevel);
        SetVertical(maxBaseLevel,minBaseLevel);

        vc.cal.WriteCommand("SOURce:VOLTage:LEVel:IMMediate:AMPLitude "+std::to_string(standardU_base));
        vc.cal.WriteCommand("OUTPut:STATe ON");

        MeasRes(standardUosc_base,sourceUosc_base);

        switch(mode)
        {
        case Mode::average:
            base=Calc(standardU_base,standardUosc_base,sourceUosc_base);
            amp=top-base;

            emit setBaseValue(base);
            emit setAmpValue(amp);

            vc.cal.WriteCommand("SOURce:VOLTage:LEVel:IMMediate:AMPLitude "+std::to_string(base));
            vc.osc.WriteCommand(":CDISplay");
            Delay(SHOW_OFF_TIME);

            break;
        case Mode::intervals:
        case Mode::points:
            GetWave('1');

            if(mode==Mode::intervals) GetValues(valuesOscBase,timeMarksBase,lengthBase);
            else                      GetValues2(valuesOscBase,timeMarksBase,lengthBase);

            Calc(standardU_base,standardUosc_base,valuesOscBase,calcBase,lengthBase);

            break;
        }

        vc.cal.WriteCommand("OUTPut:STATe OFF");

        if(interChan)       //измерение межканальной погрешности для основания
        {
            chanDeviationBase=InterChannelDeviation();
        }
    }

    if(interChan)           //измерение межканальной погрешности для вершины
    {
        vc.cal.WriteCommand("SOURce:VOLTage:LEVel:IMMediate:AMPLitude "+std::to_string(standardU_top));
        SetVertical(maxTopLevel,minTopLevel);
        chanDeviationTop=InterChannelDeviation();
    }

    ++count;

    ToStream();

    emit finished();

    mutex.unlock();
}

//запись полученных значений в stream
void CompensationMethod::ToStream()
{
    QDateTime dt=QDateTime::currentDateTime();
    QString dts=dt.toString("dd.MM.yyyy hh.mm.ss");

    if(!file.isOpen()) file.open(QFile::WriteOnly);

    stream.setEncoding(QStringConverter::System);

    stream<<dts<<'\n';

    switch(mode)
    {
        case Mode::average:
        {
            if(count==1)
            {
                stream<<dts<<'\n'
                      <<"№;"<<"Uист_вершина;"<<"Uэт_вершина;"<<"Uэт_уст(вершина);"<<"Uд_вершина;"
                      <<"Uист_основание;"<<"Uэт_основание;"<<"Uэт_уст(основание);"<<"Uд_основание;"<<"Амплитуда;"
                      <<"Межкан-ая погр.(вершина);"<<"Межкан-ая погр.(основание);"<<'\n';
            }
            stream<<count<<";"
                  <<sourceUosc_top<<";"
                  <<standardUosc_top<<";"
                  <<standardU_top<<";"
                  <<top<<";";
            if(pulse)
            {
                  stream<<sourceUosc_base<<";"
                        <<standardUosc_base<<";"
                        <<standardU_base<<";"
                        <<base<<";"
                        <<amp<<";";
            }
            else
            {
                stream<<";;;;;";
            }
            if(interChan)
            {
                stream<<chanDeviationTop<<";";
                if(pulse) stream<<chanDeviationBase<<";";
            }
            stream<<'\n';
            break;
        }
        case Mode::points:
        case Mode::intervals:
        {
            stream<<dts<<'\n'
                  <<"№(вершина);"<<"Время(вершина);"<<"Uист_вершина;"<<"Uэт_вершина;"<<"Uэт_вершина(уст.);"<<"Uд_вершина;"
                  <<"№(основание);"<<"Время(основание);"<<"Uист_основание;"<<"Uэт_основание;"<<"Uэт_основание(уст.);"<<"Uд_основание;"
                  <<"Межкан-ая погр.(вершина);"<<"Межкан-ая погр.(основание);"<<'\n';

            for(unsigned long i{0};i<lengthTop||i<lengthBase;++i)
            {
                if(i<lengthTop)
                {
                    stream<<(i+1)<<";";
                    if(timeMarksTop) stream<<timeMarksTop[i]<<";";
                    if(valuesOscTop) stream<<valuesOscTop[i]<<";";
                    if(!i)
                    {
                        stream<<standardUosc_top<<";"
                              <<standardU_top   <<";";
                    }
                    else
                    {
                        stream<<";;";
                    }
                    if(calcTop) stream<<calcTop[i]<<";";
                }
                if(i<lengthBase)
                {
                    if(i>=lengthTop) stream<<";;;;;;";
                    stream<<(i+1)<<";";
                    if(timeMarksBase) stream<<timeMarksBase[i]<<";";
                    if(valuesOscBase) stream<<valuesOscBase[i]<<";";
                    if(!i)
                    {
                        stream<<standardUosc_base<<";"
                              <<standardU_base   <<";";
                    }
                    else
                    {
                        stream<<";;";
                    }
                    if(calcBase) stream<<calcBase[i]<<";";
                }
                if(!i&&interChan)
                {
                    if(lengthBase==0) stream<<";;;;;;";
                    stream<<chanDeviationTop<<";";
                    if(pulse) stream<<chanDeviationBase<<";";
                }
                stream<<"\n";
            }
            count=0;
            break;
        }
    }
}

//получить список приборов
QStringList CompensationMethod::GetInstrList()
{
    try
    {
        return vc.FindInstruments();
    }
    catch(std::exception& e)
    {
        emit error(QString::fromStdString(e.what()));
    }
    return QStringList{};
}

//создать протокол
void CompensationMethod::Protocol()
{
    if(file.isOpen())
    {
        QDateTime dt=QDateTime::currentDateTime();
        QString dts=dt.toString("dd.MM.yyyy hh.mm.ss");

        file.rename(dts+".csv");
        file.close();
        stream.flush();

        count=0;
    }
    else
    {
        emit error("Отсутсвуют данные для записи, либо файл протокола уже создан!");
    }
}

//объединяющая функция для запуска основной процедуры в новом потоке
void CompensationMethod::FunctionWrapper()
{
    try
    {
        /* при использовании конкретных приборов можно не спрашивать у пользователя адреса, а прописать их в команде ниже,
         * например, для текущего сетапа vc.OpenSessions("USB0::0x2A8D::0x9008::MY61190105::INSTR","GPIB0::19") */
        vc.OpenSessions(oscAdress,calAdress);
        MainProcedure();
    }
    catch(std::exception& e)
    {
        if(vc.cal.isOpen) vc.cal.WriteCommand("OUTPut:STATe OFF");
        emit error(QString::fromStdString(e.what()));
    }
}
