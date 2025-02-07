#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QComboBox>
#include <QMessageBox>
#include <QThread>
#include "compensationmethod.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT
    QThread qth{};
public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();
    CompensationMethod cm{};

private slots:
    void instr_lists_activated(QComboBox*);
    void on_showAuxPanel_clicked();
    void on_start_clicked();
    void set_statusBar_text(QString);
    void set_interface_active();
    void set_top_value(double);
    void set_base_value(double);
    void set_amp_value(double);
    void show_message(QString);
    void on_record_clicked();

private:
    Ui::MainWindow *ui;
protected:
    bool eventFilter(QObject *obj,QEvent *event) override;
};

#endif // MAINWINDOW_H
