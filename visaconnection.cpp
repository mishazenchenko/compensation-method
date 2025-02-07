#include "visaconnection.h"

//проверка успешности операции с VISA
void Visa::CheckState(ViStatus status,std::string message)
{
    if(status<VI_SUCCESS) throw MyException("Ошибка VISA: "+message);
}

//деструктор
VisaSession::~VisaSession()
{
    viClose(session);
}

//отправить команду
void VisaSession::WriteCommand(std::string command)
{
    CheckState(viPrintf(session,"%s\n",command.c_str()),
               "не удается послать команду!");
    CheckState(viQueryf(session,"%s\n","%t","*OPC?",buffer),
               "возникла проблема ожидания!");
}

//запросить и получить ответ
void VisaSession::Query(std::string command)
{
    CheckState(viQueryf(session,"%s\n","%t",command.c_str(),buffer),
               "не удается осуществить запрос!");
}



//деструктор
VisaConnection::~VisaConnection()
{
    viClose(resManager);
    viClose(findList);
}

//поиск приборов и создание списка найденных приборов
QStringList VisaConnection::FindInstruments()
{
    ViChar buffer[VI_FIND_BUFLEN];
    QStringList qStrLst;

    CheckState(viOpenDefaultRM(&resManager),
               "не открывается менеджер ресурсов!");
    CheckState(viFindRsrc(resManager,"?*",&findList,&retCount,buffer),
               "не формируется список доступных приборов!");
    qStrLst.append(buffer);

    do{
        status=viFindNext(findList,buffer);
        if(status==VI_SUCCESS) qStrLst.append(buffer);
    }while(status==VI_SUCCESS);

    return qStrLst;
}

//открыть сесии осциллографа и калибратора
void VisaConnection::OpenSessions(QString oscName,QString calName)
{
    CheckState(viOpen(resManager,oscName.toLocal8Bit().data(),VI_NULL,3000,&osc.session),
               "не удается открыть сессию прибора с адресом "+oscName.toStdString()+"!");
    osc.isOpen=true;
    CheckState(viOpen(resManager,calName.toLocal8Bit().data(),VI_NULL,3000,&cal.session),
               "не удается открыть сессию прибора с адресом "+calName.toStdString()+"!");
    cal.isOpen=true;
    viSetAttribute(osc.session,VI_ATTR_TMO_VALUE,15000);
}

//получить волновую форму (для осциллографа)
void VisaConnection::GetWave(short* wave,unsigned long& dataPoints)
{
    CheckState(viQueryf(osc.session,"%s\n","%#hb",":WAVeform:DATA?",&dataPoints,wave),
               "не удается получить волновую форму!");
    viFlush(osc.session,VI_READ_BUF);
}
