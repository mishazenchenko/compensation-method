#ifndef WAVEFORM_H
#define WAVEFORM_H

#include "additional.h"
#include "myexception.h"
#include <QMainWindow>
#include <QtMath>

/* данный класс предоставляет функции для измерения параметров импульсных сигналов, таких как максимальные и минимальные значения
 * напряжения вершины и основания, моменты времени, соотвествующие определенным уровням и т.д. Используется представление импульсного
 * сигнала как волновой формы с несколькими устойчивыми состояниями (в данном случае двумя - вершиной и основанием). Для определения
 * параметров сигнала сначала рассчитывается гистограмма, она позволяет определить тип сигнала (постоянное напряжение или импульсное)*/
class Waveform
{
private:
    Waveform(){}

    constexpr static short STATES{15};        //количество проверяемых столбцов гистограммы для вынесения решения о типе сигнала
    constexpr static short MAX   {32640};     //максимальное значение точки волновой формы
    constexpr static short MIN   {-32704};    //минимальное значение точки волновой формы
    constexpr static short STEP  {1000};      //значение шага гистограммы
    constexpr static short HOLE  {32672};     //пустое (не полученное) значение

    enum class StepType{rise=1,fall=-1};    //тип перепада

    static void Hysteresis(short[],int,int,StepType,unsigned long,unsigned long,unsigned long&,unsigned long&);
    static void FindMaxMin(short[],unsigned long,unsigned long,unsigned long,unsigned long,short&,short&);

public:
    static Bundle GetSignalParams(short*,unsigned long,unsigned long);

    //сравнение двух значений
    template<typename T1,typename T2>
    static inline short Cmpr(T1 i1,T2 i2)
    {
        if(i1>i2) return 1;
        else if(i1<i2) return -1;
        else return 0;
    }

    //средняя точка между центрами двух интервалов [t1,t2] и [t3,t4]
    template<typename T>
    static inline T MeanDif(T t1,T t2,T t3,T t4)
    {
        if((Cmpr(t1,t2)!=Cmpr(t3,t4))||(Cmpr(t2,t3)!=Cmpr(t1,t2))) throw MyException("MeanDif: некорректные аргументы!");
        T temp1=(t3>t1)? t3-t1 : t1-t3;
        T temp2=(t4>t2)? t4-t2 : t2-t4;
        return temp1/2+temp2/2;
    }

    //среднее двух значений
    template<typename T>
    static inline T Mean(T t1,T t2)
    {
        return (t1+t2)/2;
    }

    //линейное преобразование значения arg
    template<typename T1, typename T2>
    static inline void Conversion(T1& arg,T2 mult,T2 add)
    {
        arg=arg*mult+add;
    }

    //поменять местами t1 и t2
    template<typename T>
    static inline void Swap(T& t1,T& t2)
    {
        T temp{t1};
        t1=t2;
        t2=std::move(temp);
    }

    //поменять местами t1 и t2, t3 и t4
    template<typename T>
    static inline void Swap(T& t1,T& t2,T& t3,T&t4)
    {
        T temp{t1};
        t1=t2;
        t2=std::move(temp);

        temp=t3;
        t3=t4;
        t4=std::move(temp);
    }
};

#endif // WAVEFORM_H
