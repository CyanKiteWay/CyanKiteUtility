namespace CyanKiteUtility
{
    public class GetVariableNameHelper
    {
        public static string GetMemberName<T>(System.Linq.Expressions.Expression<System.Func<T>> memberExpression)
        {
            System.Linq.Expressions.MemberExpression expressionBody = (System.Linq.Expressions.MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
}

/*
public static class MemberInfoGetting
{
    public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
    {
        MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
        return expressionBody.Member.Name;
    }
}

string TableName = "123";
string nameOfTestVariable = MemberInfoGetting.GetMemberName(() => TableName);

//最后得到 nameOfTestVariable  = "TableName"

static void Main(string[] args) 
{
  GetName(new { var1 });
  GetName2(() => var1);
  GetName3(() => var1);
}

static string GetName<T>(T item) where T : class 
{
  return typeof(T).GetProperties()[0].Name;
}

static string GetName2<T>(Expression<Func<T>> expr) 
{
  return ((MemberExpression)expr.Body).Member.Name;
}

static string GetName3<T>(Func<T> expr) 
{
  return expr.Target.GetType().Module.ResolveField(BitConverter.ToInt32(expr.Method.GetMethodBody().GetILAsByteArray(), 2)).Name;
}
*/