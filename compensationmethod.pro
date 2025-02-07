<<<<<<< HEAD
QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++17

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    compensationmethod.cpp \
    main.cpp \
    mainwindow.cpp \
    visaconnection.cpp \
    waveform.cpp

HEADERS += \
    additional.h \
    compensationmethod.h \
    mainwindow.h \
    myexception.h \
    visaconnection.h \
    waveform.h

FORMS += \
    mainwindow.ui

INCLUDEPATH += $$PWD/Include
DEPENDPATH += $$PWD/Include

win32: LIBS += $$PWD/visa64.lib
unix:  LIBS += -l$$PWD/librsvisa.so
=======
QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

CONFIG += c++17

# You can make your code fail to compile if it uses deprecated APIs.
# In order to do so, uncomment the following line.
#DEFINES += QT_DISABLE_DEPRECATED_BEFORE=0x060000    # disables all the APIs deprecated before Qt 6.0.0

SOURCES += \
    compensationmethod.cpp \
    main.cpp \
    mainwindow.cpp \
    visaconnection.cpp \
    waveform.cpp

HEADERS += \
    additional.h \
    compensationmethod.h \
    mainwindow.h \
    myexception.h \
    visaconnection.h \
    waveform.h

FORMS += \
    mainwindow.ui

INCLUDEPATH += $$PWD/Include
DEPENDPATH += $$PWD/Include

win32: LIBS += $$PWD/visa64.lib
unix:  LIBS += -l$$PWD/librsvisa.so
>>>>>>> 368ab02 (bash commit)
