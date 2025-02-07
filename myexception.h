#ifndef MYEXCEPTION_H
#define MYEXCEPTION_H

#include <exception>
#include <string>

//класс собственного исключения
class MyException : public std::exception
{
private:
    std::string message;
public:
    MyException(std::string qs) : std::exception(), message{qs} {};

    const char* what() const noexcept override
    {
        return message.c_str();
    }
};

#endif // MYEXCEPTION_H
