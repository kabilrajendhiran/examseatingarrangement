using System.Threading.Tasks;

namespace ExamSeatingArrangement2020.Repository
{
    public interface IIntermediateRepository
    {
        public Task<string> GetFilePath(int id);

        public Task<int> FillInterMediateTables(string filepath);

        public Task FillExamTable(string filepath);

        public Task FillSeatingTable(string filepath);

        public Task SetFinished(int id);
    }
}