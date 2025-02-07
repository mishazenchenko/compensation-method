#ifndef ADDITIONAL_H
#define ADDITIONAL_H

//тип исследуемого сигнала: dc - постоянное напряжение, pulse - импульс
enum class SignalType{dc,pulse};

/* режим измерений: average - усреднение всей волновой формы на экране осциллографа,
 * intervals - усреднение всех точек, приходящихся на интервалы заданной длительности,
 * points - значения волновой формы в моменты времени, образованные разбиением волновой формы на интервалы заданной длительности*/
enum class Mode{average,intervals,points};

//разрядность АЦП осциллографа
enum class Resolution{BITS10,BITF11,BITF12,BITF13,BITF14,BITF15,BITF16};

//объединение параметров волновой формы
struct Bundle
{
    unsigned long widthPositive,widthNegative,riseTime,fallTime;
    short maxTopLevel,minTopLevel,maxBaseLevel,minBaseLevel;
    SignalType sT;
};

#endif // ADDITIONAL_H
