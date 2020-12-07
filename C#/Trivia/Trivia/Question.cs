namespace Trivia
{
    internal class Question
    {
        public string Category { get; }
        public string Text { get; }

        public Question(string category, string text)
        {
            Category = category;
            Text = text;
        }
    }
}