namespace DigitalInventory;

public static class Factory<T, U> where T : class, U, new()
                                  where U : IPrimaryProperties
{
    public static U GetInstance()
    {
        U objT;
        objT = new T();
        return objT;
    }

}