namespace task01;

public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }
        int length= input.Length;
        string new_input = "";
        for (int i = 0; i < length; i ++)
        {
            if (Char.IsPunctuation(input[i]) == false && Char.IsWhiteSpace(input[i]) == false) {
                new_input += input[i];
            }
        }
        length = new_input.Length;
        new_input = new_input.ToLower();
        bool flag = true;
        
        if (length % 2 == 0)
        {
            for (int i = 0; i < length/2; i++)
            {
                if (new_input[i] != new_input[length-(i+1)])
                {
                    flag = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < (length-1)/2; i++)
            {
                if (new_input[i] != new_input[length-(i+1)])
                {
                    flag = false;
                }
            }
        }
        
        return flag;
    }
}