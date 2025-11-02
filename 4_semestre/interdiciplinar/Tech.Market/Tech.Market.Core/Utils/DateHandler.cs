using Dapper;
using System.Data;

namespace Tech.Market.Core.Utils
{

    public static class HandlerDapper
    {
        public static void ApplyHandlerDapper()
        {
            SqlMapper.AddTypeHandler<DateOnly>(new DateHandler());
        } 
    }

    public class DateHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override DateOnly Parse(object value)
        {
            return DateOnly.TryParse(value.ToString(), out DateOnly result) ? result : default;
        }

        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.DbType = DbType.Date;
            parameter.Value = value.ToDateTime(default).Date.ToString("yyyy-MM-dd");
        }
    }

}

