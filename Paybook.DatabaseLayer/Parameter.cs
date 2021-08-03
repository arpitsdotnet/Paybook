namespace Paybook.DatabaseLayer
{
    public class Parameter
    {
        public string Key = "";
        public object Value = "";
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_Key"></param>
        /// <param name="_Value"></param>
        public Parameter(string _Key, object _Value)
        {
            Key = _Key;
            Value = _Value;
        }

    }
}
