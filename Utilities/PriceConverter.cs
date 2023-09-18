namespace MyAutoService.Utilities
{
    public static class PriceConverter
    {
         public static string ToToman(this int  price)
        {
            return price.ToString("#,0 تومان");
        }      
        
        public static string ToToman(this double  price)
        {
            return price.ToString("#,0 تومان");
        }
    }
}
