#ifndef COMPENSATIONMETHOD_H
#define COMPENSATIONMETHOD_H

#include <QString>
#include <QFile>
#include <QTextStream>
#include <QDateTime>
#include <QThread>
#include <QMutex>
#include <QWaitCondition>
#include "visaconnection.h"
#include "additional.h"

class CompensationMethod : public QObject
{
    Q_OBJECT

private:
    const double DC_TIMEBASE           {0.001};        //значение длительности отображаемой волновой формы при измерениях постноянных напряжений в секундах
    const short PRE_MEASURE_TIMEOUT    {1};            //длительность ожидания перед накоплением значений для собственных измерений осциллографа в секундах
    const short POST_MEASURE_TIMEOUT   {5};            //длительность накопления значений для собственных измерений осциллографа в секундах
    const short SHOW_OFF_TIME          {3};            /* время, в течение которого будет отображаться уточненное напряжение калибратора (только для Mode::average),
                                                       * не влияет на измерения */
    const short POST_AUTOSCALE_TIMEOUT {2};            //время ожидания после AutoScale
    const unsigned long MAX_WAVE_POINTS{500'000'000};  //максимально возможное количество точек волновой формы
    unsigned long count                {0};            //номер измерения
    unsigned long dataPoints,lengthTop,lengthBase;      /* соответственно: количество точек волновой формы; количество точек волновой формы, приходящихся на вершину;
                                                         * количество точек волновой формы, приходящихся на основание */
    double yIncrement, yOrigin, xIncrement, xOrigin;    //коэффициенты для пересчета массива значений волновой формы в реальные

    /* различные значений напряжения: standard - напряжение калибратора, source - напряжение источника;
     * osc - напряжение, измеренное осциллографом (без этой приписки - рассчитанное значение)
     * chanDeviationTop - межканальная погрешность для вершины, chanDeviationBase - межканальная погрешность для основания */
    double standardU_top,standardUosc_top,sourceUosc_top,standardU_base,standardUosc_base,sourceUosc_base,
           chanDeviationTop,chanDeviationBase;
    double top,base,amp;                //рассчитанные значения вершины, основания и амплитуды (только для Mode::average)
    double* valuesOscTop     {nullptr}; //массив измеренных значений вершины импульсного напряжения источника или постоянного напряжения (для Mode::intervals и  Mode::points)
    double* valuesOscBase    {nullptr}; //массив измеренных значений основания импульсного напряжения источника (для Mode::intervals и  Mode::points)
    double* calcTop          {nullptr}; //массив пересчитанных значений напряжения вершины импульсного напряжения источника или постоянного напряжения (для Mode::intervals и  Mode::points)
    double* calcBase         {nullptr}; //массив пересчитанных значений основания импульсного напряжения источника (для Mode::intervals и  Mode::points)
    short* wave              {nullptr}; //волновая форма
    QString* timeMarksTop    {nullptr}; //массив с времннЫми метками вершины импульсного напряжения или постоянного напряжения (для Mode::intervals и  Mode::points)
    QString* timeMarksBase   {nullptr}; //массив с времннЫми метками основания импульсного напряжения (для Mode::intervals и  Mode::points)
    QFile file{"dummy"};                //файл, в который будут записаны результаты измерений
    QTextStream stream{&file};          //поток, используемый для записи файла
    VisaConnection vc;

    template<typename T>
    T MeanValue(T*,unsigned long);

    void GetWave(const char);
    void GetValues(double*&,QString*&,unsigned long&);
    void GetValues2(double*&,QString*&,unsigned long&);
    void SetVertical(double,double);
    void Delay(double);
    void SetTimebase(double,double,double);
    void MeasRes(double&,double&);
    void ToStream();
    void MainProcedure();
    QString MeasRes();
    double Calc(double,double,double);
    void Calc(double,double,double*,double*&,unsigned long);
    double InterChannelDeviation();

public:
    bool imp_50Ohm  {false};    //флаг 50-омного выхода источника исследуемого сигнала
    bool interChan  {false};    //флаг измерения межканальной погрешности
    bool averaging  {false};    //флаг собственного усреднения осциллографа
    bool pulse      {false};    //флаг импульсного исследуемого напряжения
    bool goFurther  {false};    //флаг продолжения CompensationMethod::MainProcedure() (для промптов)
    int averagingNum;           //количество усредняемых волновых форм при averaging=true
    double skew;                //сдвиг фронта сигнала
    double intervalTime;        //значение интервала (для Mode::intervals и  Mode::points)
    QString calAdress;          //строка с адресом калибратора
    QString oscAdress;          //строка с адресом осциллографа
    Mode mode;                  //режим измерений
    Resolution resolution;      //разрешение АЦП осциллографа
    QWaitCondition qwc;         //для остановки CompensationMethod::MainProcedure() во время промптов

    ~CompensationMethod();
    QStringList GetInstrList();
    void FunctionWrapper();
    void Protocol();

signals:
    void finished();
    void error(QString);
    void setTopValue(double);
    void setBaseValue(double);
    void setAmpValue(double);
    void message(QString);
};

#endif // COMPENSATIONMETHOD_H
