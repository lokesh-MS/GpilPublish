using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPILWebApp.ViewModel
{
    public class GLTStemMoisture
    {
//        [Crop],[Variety],[StripGrade],[StemGrade],[Date],[Time],[EquipNo],[RunCaseNo],[MoistureResult],[AfterCorr],[CaseTemp],[attribute1],
//[attribute2],[attribute3],[attribute4],[attribute5]
        public int Crop { get; set; }
        public int Variety { get; set; }
        public int StripGrade { get; set; }
        public int StemGrade { get; set; }
        public int Date { get; set; }
        public int RunCaseNo { get; set; }
        public int Time { get; set; }
        public int EquipNo { get; set; }
        public int MoistureResult { get; set; }
        public int AfterCorr { get; set; }
        public int CaseTemp { get; set; }
      


    }
    public class ListGLTStemMoisture
    {
        public List<GLTStemMoisture> StemMoistures { get; set; }
    }
}