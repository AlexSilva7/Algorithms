bool CheckIfInputArePalindrome(string input)
{
    var replaced = input.ToLower();
    char[] caracteres = replaced.ToCharArray();
    Array.Reverse(caracteres);
    var reverse = new string(caracteres);
    
    if(replaced == reverse) return true;

    return false;
}

//Console.WriteLine(CheckIfInputArePalindrome("arara"));