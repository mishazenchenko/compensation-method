#ifndef VISACONNECTION_H
#define VISACONNECTION_H

#include <QString>
#include <QStringList>
#include <visa.h>
#include "myexception.h"

class Visa
{
protected:
    void CheckState(ViStatus status,std::string message);
};

class VisaSession : public Visa
{
public:
    bool isOpen{false};             //флаг открытой сессии
    ViSession session;              //сессия VISA
    ViChar buffer[VI_FIND_BUFLEN];  //массив, содержащий ответную строку

    ~VisaSession();
    void WriteCommand(std::string);
    void Query(std::string);
};

class VisaConnection : public Visa
{ 
private:
    ViFindList findList;        //список найденных приборов
    ViStatus status;            //статус (используется для проверки успешности взаимодействия с прибором)
    ViSession resManager;       //менеджер ресурсов (используется для поиска приборов)
    ViUInt32 retCount;          //количество найденных приборов

public:
    VisaSession osc;            //сессия осциллографа
    VisaSession cal;            //сессия калибратора

    ~VisaConnection();
    void OpenSessions(QString,QString);
    void GetWave(short*,unsigned long&);
    QStringList FindInstruments();
};

#endif // VISACONNECTION_H
