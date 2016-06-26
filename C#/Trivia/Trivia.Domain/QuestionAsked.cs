namespace Trivia.Domain
{
    public class QuestionAsked
    {
        public string Question { get; private set; }

        public QuestionAsked(string question)
        {
            Question = question;
        }
    }
}