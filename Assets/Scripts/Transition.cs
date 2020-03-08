using System.Collections.Generic;

public abstract class Transition
{
    public abstract bool isTriggered();
    public abstract string getTargetState();
    public abstract void getActions();
}