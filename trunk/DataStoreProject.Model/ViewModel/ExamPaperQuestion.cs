using DataStoreProject.Model.Entity.ExamPaper;
using DataStoreProject.Model.Entity.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class ExamPaperQuestion
    {
        public W_ExamPaper PaperInfo { get; set; }
        public decimal TotalScore { get; set; }
        public double TotalTime { get; set; }
        public int ResultId { get; set; }
        public List<ExamQuestionType> QuestionType { get; set; } 
    }

    public class ExamQuestionType
    {
        public W_ExamPaperDetail TypeInfo { get; set; }
        public List<Question> Question { get; set; }
    }
}
