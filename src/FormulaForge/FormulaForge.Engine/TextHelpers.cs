namespace FormulaForge.Engine;

public static class TextHelpers
{
    extension(char c)
    {
        public bool IsLetterOrUnderscore => char.IsLetter(c) || c == '_';
    
        public bool IsLetterOrUnderscoreOrDigit => char.IsLetterOrDigit(c) || c == '_';
    }
}