using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateIn.Console.DAL
{
    public class ContCardPic
    {
        public long ContCardPicID { get; set; }
        public long ContCardID { get; set; }
        public DateTime PicDtm { get; set; }
        public string PicName { get; set; }
        public byte[] PicData { get; set;  }
        public ContCardPic()
        {
            ContCardPicID = 0;
            ContCardID = 0;
            PicDtm = DateTime.MinValue;
            PicName = string.Empty;
            PicData = new byte[0];
        }
    }
}
