#include <iostream>
#include <string>
#include <cstdlib>

using namespace std;

class Parent
{
    public:
    virtual void print()
    {
        std::cout<<"parrent\n";

    };
};

class Child: Parent
{
    public:
    virtual void print() override
    {
        __super::print();
        std::cout<<"child\n";
    };
};

int main() {
    Child* obj = new Child();
    obj->print();
    return 0;
}
