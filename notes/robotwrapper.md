# robotWrapper implementation
## Removed function
### Duplicate exists
```cs
public void DoActionSetArc(float radius, float distance, int direction)
{
    Log.Warning("Go set arc !");
    GetManualControlTask().SetArc(radius, distance, direction);
}
```

## Unimplemented functions in mqtt api
```cs
private SystemTask InsertTaskOnTopOfTheStack(Task task)
public void SetRoll(uint position)
{
    LogicCore.PostEvent(new RollSetPositionEvent(position));
}

public void EnableRollSafety()
{
    LogicCore.PostEvent(new RollSafetyEnabledEvent());
}

public void DisableRollSafety()
{
    LogicCore.PostEvent(new RollSafetyDisabledEvent());
}

public void CompleteRollSafety()
{
    LogicCore.PostEvent(new RollSafetyCompletedEvent());
}
public void EnableRTK(RtkConnection connection)
{
    LogicCore.Navigation.EnableRTK(connection);
}
public void DoVsbSendConfig(EventHandler<VsbConfigCompleteEventArgs> callback)
{
    VsbSendConfigTask vscTask = new VsbSendConfigTask();
    vscTask.VsbConfigCompleted += callback;
    SystemTask wrapper = new SystemTask(){ SubTasks = { vscTask } };
    LogicCore.RootTask.Insert(wrapper);
}
public MissionScheduler DoValidateAction()

// all in #region Wizard Screen

public ScreenContent GetScreen()

public void PostWarningWarranty()
{
    LogicCore.PostFault(new WarningMessage("WarrantyWarningBlockedHead"));
}
public ITool[] RobotTools
{
    get
    {
        return LogicCore.Tools;
    }
}

public MissionConfig[] MissionFields
{
    get { return LogicCore.Navigation.UserParameters.MissionFields; }
    set { LogicCore.Navigation.UserParameters.MissionFields = value; }
}
```

## Getters
```cs
public Task DoSearchCurrentParcel()
public float getCuttingHeadCalibration(int id)
public bool GetCuttingHeightCompleted()
public bool IsDiscoverStationsTaskCompleted()
// all from 866
```
## Weird names ?
```cs
public void DoActionMotorDriveIdleMode(int mode) =>
{
    Clear();
    Add(new IdlePrimitiveTask((MotorDriveIdleMode)mode));
}
```
