using Paybook.DatabaseLayer.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Common
{
    public interface ILastSavedIdProcessor
    {
        string GetLastSavedID(string type);
        void LastSavedID_Update(string sID, string type);
    }

    public class LastSavedIdProcessor : ILastSavedIdProcessor
    {

        private readonly ILastSavedIdRepository _lastSavedIdRepo;

        public LastSavedIdProcessor()
        {
            _lastSavedIdRepo = new LastSavedIdRepository();
        }

        public string GetLastSavedID(string type)
        {
            string sPrefix = "", sSeperator = "", sNewNumber = "";
            int iCurrentyear = DateTime.Now.Year;
            try
            {

                DataTable dt = _lastSavedIdRepo.GetByType(type);
                if (dt != null && dt.Rows.Count > 0)
                {
                    sPrefix = dt.Rows[0]["Prefix"].ToString();
                    int iLastnumber = Int32.Parse(dt.Rows[0]["LastNumber"].ToString());
                    int iLastYear = Int32.Parse(dt.Rows[0]["Year"].ToString());
                    sSeperator = dt.Rows[0]["Seperator"].ToString();

                    if (iLastYear < iCurrentyear)
                        sNewNumber = "1";
                    else
                    {
                        sNewNumber = (iLastnumber + 1).ToString();
                    }

                }

                if (sNewNumber.Length == 1)
                    sNewNumber = "000" + sNewNumber;
                if (sNewNumber.Length == 2)
                    sNewNumber = "00" + sNewNumber;
                if (sNewNumber.Length == 3)
                    sNewNumber = "0" + sNewNumber;

                string sCurrentYear = iCurrentyear.ToString();

                string sID = sPrefix + sSeperator + sCurrentYear + sSeperator + sNewNumber;


                return sID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LastSavedID_Update(string sID, string type)
        {
            try
            {
                _lastSavedIdRepo.Update(sID, type);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
