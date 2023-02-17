namespace LambdaExpressions;
public class Function<TParameter1, TReturn>
{
    public TParameter1? Parameter1 { get; set; }
    public TReturn? Return { get; set; }
}

public class Function<TParameter1, TParameter2 ,TReturn>
{
    public TParameter1? Parameter1 { get; set; }
    public TParameter2? Parameter2 { get; set; }
    public TReturn? Return { get; set; }
}

public class Function<TParameter1, TParameter2, TParameter3, TReturn>
{
    public TParameter1? Parameter1 { get; set; }
    public TParameter2? Parameter2 { get; set; }
    public TReturn? Return { get; set; }
}
