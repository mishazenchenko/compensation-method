#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->osc->installEventFilter(this);
    ui->standard->installEventFilter(this);
    ui->dockWidget->setVisible(false);
    this->setFixedHeight(250);

    cm.moveToThread(&qth);
    connect(&qth,&QThread::started,                 &cm,&CompensationMethod::FunctionWrapper);
    connect(&cm,&CompensationMethod::finished,      &qth,&QThread::quit);
    connect(&cm,&CompensationMethod::error,         &cm,&CompensationMethod::finished);
    connect(&cm,&CompensationMethod::error,         this,&MainWindow::set_statusBar_text);
    connect(&cm,&CompensationMethod::finished,      this,&MainWindow::set_interface_active);
    connect(&cm,&CompensationMethod::setTopValue,   this,&MainWindow::set_top_value);
    connect(&cm,&CompensationMethod::setBaseValue,  this,&MainWindow::set_base_value);
    connect(&cm,&CompensationMethod::setAmpValue,   this,&MainWindow::set_amp_value);
    connect(&cm,&CompensationMethod::message,       this,&MainWindow::show_message);
}

//поиск приборов осуществляется каждый раз при отображении выпадающего списка с адресами
bool MainWindow::eventFilter(QObject *obj, QEvent *event)
{
    if((obj==ui->standard||obj==ui->osc)&&event->type()==QEvent::MouseButtonPress)
    {
        instr_lists_activated(static_cast<QComboBox*>(obj));
    }
    return false;
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::instr_lists_activated(QComboBox *cbox)
{
    cbox->clear();
    cbox->addItems(cm.GetInstrList());
}

void MainWindow::on_showAuxPanel_clicked()
{
    bool flag=ui->dockWidget->isVisible();
    ui->dockWidget->setVisible(!flag);
    if(flag) this->setFixedHeight(250);
    else this->setFixedHeight(580);
}

void MainWindow::set_statusBar_text(QString text)
{
    ui->statusbar->showMessage(text,5000);
}

void MainWindow::set_interface_active()
{
    this->setEnabled(true);
}

void MainWindow::set_top_value(double d)
{
    ui->top->setText(QString::number(d));
}

void MainWindow::set_base_value(double d)
{
    ui->base->setText(QString::number(d));
}

void MainWindow::set_amp_value(double d)
{
    ui->amp->setText(QString::number(d));
}

//можно немного красивей сделать промпт: заменить кнопки "Yes" и "No" на что-нибудь на русском, прописать заголовок всплывающего окна
void MainWindow::show_message(QString qstr)
{
    QMessageBox msgBox;
    msgBox.setText(qstr);
    msgBox.setStandardButtons(QMessageBox::Yes|QMessageBox::No);
    cm.goFurther=(QMessageBox::Yes==msgBox.exec());
    cm.qwc.wakeAll();
}

//установка параметров измерений, подключение приборов и запуск основной процедуры по нажатию на кнопку "Начать"
void MainWindow::on_start_clicked()
{
    this->setEnabled(false);

    ui->top->clear();
    ui->base->clear();
    ui->amp->clear();

    cm.oscAdress=   ui->osc->currentText();
    cm.calAdress=   ui->standard->currentText();
    cm.imp_50Ohm=   ui->rb50Ohm->isChecked();
    cm.averaging=   ui->averageing->isChecked();
    cm.interChan=   ui->interChannelDev->isChecked();
    cm.averagingNum=ui->averagingNum->value();
    cm.skew=        ui->skew->value()/1E6;
    cm.intervalTime=ui->interval->value()/1E6;

    if(ui->rbAverage->isChecked())         cm.mode=Mode::average;
    if(ui->rbIntervalAverage->isChecked()) cm.mode=Mode::intervals;
    if(ui->rbIntervalInstant->isChecked()) cm.mode=Mode::points;

    if(ui->_10bit->isChecked())            cm.resolution=Resolution::BITS10;
    if(ui->_11bit->isChecked())            cm.resolution=Resolution::BITF11;
    if(ui->_12bit->isChecked())            cm.resolution=Resolution::BITF12;
    if(ui->_13bit->isChecked())            cm.resolution=Resolution::BITF13;
    if(ui->_14bit->isChecked())            cm.resolution=Resolution::BITF14;
    if(ui->_15bit->isChecked())            cm.resolution=Resolution::BITF15;
    if(ui->_16bit->isChecked())            cm.resolution=Resolution::BITF16;

    qth.start();
}

//создание протокола по нажатию на кнопку "Отчет"
void MainWindow::on_record_clicked()
{
    cm.Protocol();
}

