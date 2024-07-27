using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void CreateTestData(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogAppDbContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag("web prograglama", TagColors.grey),
                        new Tag("full-stack", TagColors.cornflowerblue),
                        new Tag("game", TagColors.darkgoldenrod),
                        new Tag("backend", TagColors.cadetblue),
                        new Tag("frontend", TagColors.mediumpurple)
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                    new User("ahmetKaya", "Ahmet", "Kaya", "ahmet.kaya@example.com", "111111", "652aec00e482bcee4d6714b15e9946d8.jpg"),
                    new User("ayseYildiz", "Ayşe", "Yıldız", "ayse.yildiz@example.com", "111111", "d0cbd1380c72ddf3750c896433b2dea1.jpg"),
                    new User("mehmetDemir", "Mehmet", "Demir", "mehmet.demir@example.com", "111111", "0733ba760b29378474dea0fdbcb97107.jpg"),
                    new User("fatmaCelik", "Fatma", "Çelik", "fatma.celik@example.com", "111111", "f6ee4756aee86d893ce0b48975169eed.jpg"),
                    new User("omerKara", "Ömer", "Kara", "omer.kara@example.com", "111111", "ed63467e6ecc7e2064844a7d33099b97.jpg"),
                    new User("leylaUslu", "Leyla", "Uslu", "leyla.uslu@example.com", "111111", "2458517dd26944f22ee05ab1e51e6619.jpg"),
                    new User("aliOzkan", "Ali", "Özkan", "ali.ozkan@example.com", "111111", "404d4cf7e13863bb68ab412f55d88233.jpg"),
                    new User("merveAydin", "Merve", "Aydın", "merve.aydin@example.com", "111111", "cf7648e9a0659c29bad93fa898bfbba7.jpg"),
                    new User("hasanGul", "Hasan", "Gül", "hasan.gul@example.com", "111111", "0cf95ea5de3264bbd07f35adc902bc13.jpg"),
                    new User("elifYilmaz", "Elif", "Yılmaz", "elif.yilmaz@example.com", "111111", "c74dc3af23e22a1ae5837e1196692d51.jpg"));
                    context.SaveChanges();
                }

                if (!context.Blogs.Any())
                {
                    var Tags = context.Tags.ToList();

                    var Blog1 = new Blog("Asp.net Core ile Web Geliştirme",
                        "Asp.net Core, Microsoft tarafından geliştirilmiş güçlü bir framework'tür. MVC yapısı, Razor sayfaları ve API desteği ile modern web uygulamaları oluşturmanıza olanak tanır. Ayrıca, performans ve güvenlik konularında da birçok avantaj sunar. Asp.net Core'un sunduğu esneklik ve genişletilebilirlik sayesinde, geliştiriciler ihtiyaçlarına göre özelleştirme yapabilir. ASP.NET Core, modüler yapısıyla dikkat çeker ve geniş bir eklenti desteği sunar. Bu da geliştiricilerin ihtiyaçlarına uygun çözümler üretmelerini sağlar.",
                        "Asp.net Core ile modern web uygulamaları geliştirin.", "0e4fdce8ac22e09688c580e5bc4dcd7d.jpg", 1);
                    var Comment1 = new Comment("Gayet iyi bir anlatım olmuş.", 1, 9);
                    var Comment2 = new Comment("Asp.net Core'u öğrenmek için iyi bir başlangıç noktası.", 1, 2);
                    Blog1.Comments.Add(Comment1);
                    Blog1.Comments.Add(Comment2);
                    Blog1.Tags.AddRange(Tags.Take(3).ToList());
                    Blog1.LikeCount = 5;

                    context.Blogs.Add(Blog1);
                    context.Comments.AddRange(Comment1, Comment2);
                    context.SaveChanges();

                    var Blog2 = new Blog("Unity ile Oyun Geliştirme Rehberi",
                        "Unity, popüler bir oyun motorudur ve kullanıcı dostu arayüzü ile oyun geliştiricilerinin favorisi haline gelmiştir. Unity, 2D ve 3D oyun geliştirme desteği sunar ve çok çeşitli platformlara dağıtım yapma olanağı sağlar. Unity'nin güçlü grafik motoru, fizik motoru ve ses desteği ile yüksek kaliteli oyunlar oluşturmak mümkündür. Ayrıca, Unity Asset Store'dan birçok varlık ve araç temin edilebilir. Bu makalede, Unity'nin temel bileşenlerinden ve oyun geliştirme sürecinden bahsediyoruz.",
                        "Unity ile oyun geliştirme konusunda temel bilgiler.", "0ea2725f5cf978154ec8f450cfa8fbd8.jpg", 2);
                    var Comment3 = new Comment("Harika bir oyun rehberi olmuş.", 2, 2);
                    var Comment4 = new Comment("Unity'nin bu kadar güçlü olduğunu bilmiyordum.", 2, 3);
                    Blog2.Comments.Add(Comment3);
                    Blog2.Comments.Add(Comment4);
                    Blog2.Tags.AddRange(Tags.Take(2).ToList());
                    Blog2.LikeCount = 4;

                    context.Blogs.Add(Blog2);
                    context.Comments.AddRange(Comment3, Comment4);
                    context.SaveChanges();

                    var Blog3 = new Blog("Python ile Veri Bilimi",
                        "Python, veri bilimi alanında en çok tercih edilen programlama dillerinden biridir. Zengin kütüphane desteği, veri analizi ve işleme işlemlerini kolaylaştırır. Pandas, NumPy, Matplotlib gibi kütüphaneler, veri bilimcilerin günlük çalışmalarında sıkça kullandığı araçlardır. Python'un geniş topluluğu ve açık kaynaklı doğası, veri bilimi projelerinde hızlı ve etkili çözümler üretmeyi mümkün kılar. Bu yazıda, Python ile veri bilimi üzerine detaylı bir inceleme yapacağız.",
                        "Python ile veri bilimi hakkında temel bilgiler.", "1feb841b63baa45b6106b0bac0613de3.jpg", 3);
                    var Comment5 = new Comment("Python'un veri bilimi için ne kadar uygun olduğunu öğrenmiş oldum.", 3, 4);
                    var Comment6 = new Comment("Veri bilimi için Python öğrenmek istiyorum.", 3, 5);
                    Blog3.Comments.Add(Comment5);
                    Blog3.Comments.Add(Comment6);
                    Blog3.Tags.AddRange(Tags.Skip(1).Take(3).ToList());
                    Blog3.LikeCount = 6;

                    context.Blogs.Add(Blog3);
                    context.Comments.AddRange(Comment5, Comment6);
                    context.SaveChanges();

                    var Blog4 = new Blog("JavaScript ile Dinamik Web Sayfaları",
                        "JavaScript, web geliştirme dünyasında en çok kullanılan programlama dillerinden biridir. Dinamik ve etkileşimli web sayfaları oluşturmak için kullanılan JavaScript, hem front-end hem de back-end geliştirme süreçlerinde önemli bir rol oynar. ES6 ile gelen yeni özellikler, JavaScript'in gücünü daha da artırmıştır. Bu yazıda, JavaScript'in temel özelliklerini ve kullanım alanlarını keşfedeceğiz. Ayrıca, popüler JavaScript framework'leri hakkında bilgi vereceğiz.",
                        "JavaScript ile dinamik web sayfaları oluşturma.", "3c8cacc4572c2c1a787f49576bcf0703.jpg", 4);
                    var Comment7 = new Comment("JavaScript'in gücü gerçekten etkileyici.", 4, 6);
                    var Comment8 = new Comment("Bu makale, JavaScript öğrenmek için harika bir rehber.", 4, 7);
                    Blog4.Comments.Add(Comment7);
                    Blog4.Comments.Add(Comment8);
                    Blog4.Tags.AddRange(Tags.Skip(2).Take(3).ToList());
                    Blog4.LikeCount = 7;

                    context.Blogs.Add(Blog4);
                    context.Comments.AddRange(Comment7, Comment8);
                    context.SaveChanges();

                    var Blog5 = new Blog("Mobil Uygulama Geliştirme",
                        "Mobil uygulama geliştirme, günümüzde oldukça popüler bir alan haline gelmiştir. iOS ve Android platformları için uygulama geliştirmek, geliştiriciler için büyük fırsatlar sunmaktadır. Swift ve Kotlin gibi modern diller, bu platformlarda uygulama geliştirmek için yaygın olarak kullanılmaktadır. Bu makalede, mobil uygulama geliştirmenin temel prensiplerinden ve yaygın kullanılan araçlardan bahsedeceğiz. Ayrıca, mobil uygulama geliştirme sürecinde dikkat edilmesi gereken önemli noktaları da ele alacağız.",
                        "Mobil uygulama geliştirme hakkında detaylı bilgiler.", "4d8c559a20d1bc6e505d940288261648.jpg", 5);
                    var Comment9 = new Comment("Mobil uygulama geliştirme hakkında çok şey öğrendim.", 5, 8);
                    var Comment10 = new Comment("iOS ve Android geliştirme hakkında daha fazla bilgi almak istiyorum.", 5, 9);
                    Blog5.Comments.Add(Comment9);
                    Blog5.Comments.Add(Comment10);
                    Blog5.Tags.AddRange(Tags.Skip(3).Take(2).ToList());
                    Blog5.LikeCount = 8;

                    context.Blogs.Add(Blog5);
                    context.Comments.AddRange(Comment9, Comment10);
                    context.SaveChanges();

                    var Blog6 = new Blog("Yapay Zeka ve Makine Öğrenmesi",
                        "Yapay zeka ve makine öğrenmesi, teknolojinin en hızlı gelişen alanlarından biridir. Bu teknolojiler, çeşitli sektörlerde devrim yaratmaktadır. Makine öğrenmesi, veri analitiği ve tahmine dayalı modellemelerde kullanılırken, yapay zeka ise doğal dil işleme, görüntü tanıma ve robotik gibi alanlarda kullanılmaktadır. Bu yazıda, yapay zeka ve makine öğrenmesinin temel prensiplerini, uygulama alanlarını ve gelecekteki potansiyelini inceleyeceğiz.",
                        "Yapay zeka ve makine öğrenmesi üzerine genel bir bakış.", "5c341ff2628d6b01bad2cb0c10f521d7.jpg", 6);
                    var Comment11 = new Comment("Yapay zeka hakkında çok bilgilendirici bir yazı olmuş.", 6, 8);
                    var Comment12 = new Comment("Makine öğrenmesi ile ilgili daha fazla öğrenmek istiyorum.", 6, 7);
                    Blog6.Comments.Add(Comment11);
                    Blog6.Comments.Add(Comment12);
                    Blog6.Tags.AddRange(Tags.Take(3).ToList());
                    Blog6.LikeCount = 9;

                    context.Blogs.Add(Blog6);
                    context.Comments.AddRange(Comment11, Comment12);
                    context.SaveChanges();

                    var Blog7 = new Blog("Bulut Bilişim ve AWS",
                        "Bulut bilişim, günümüzün en önemli teknolojik gelişmelerinden biridir. Amazon Web Services (AWS), bulut hizmetleri sağlayıcıları arasında lider konumdadır. AWS, esnekliği, güvenliği ve ölçeklenebilirliği ile tanınır. Bu makalede, bulut bilişim kavramını ve AWS'nin sunduğu temel hizmetleri inceleyeceğiz. Ayrıca, AWS üzerinde uygulama geliştirme ve yönetme konularına da değineceğiz.",
                        "Bulut bilişim ve AWS hakkında genel bilgiler.", "21cf0d97aee5c8856367ceac579bde09.jpg", 7);
                    var Comment13 = new Comment("Bulut bilişim hakkında bilgi sahibi oldum.", 7, 6);
                    var Comment14 = new Comment("AWS kullanarak nasıl projeler geliştirebileceğim hakkında fikirler edindim.", 7, 5);
                    Blog7.Comments.Add(Comment13);
                    Blog7.Comments.Add(Comment14);
                    Blog7.Tags.AddRange(Tags.Skip(1).Take(2).ToList());
                    Blog7.LikeCount = 10;

                    context.Blogs.Add(Blog7);
                    context.Comments.AddRange(Comment13, Comment14);
                    context.SaveChanges();

                    var Blog8 = new Blog("Veritabanı Yönetim Sistemleri",
                        "Veritabanı yönetim sistemleri (DBMS), verilerin düzenli bir şekilde saklanmasını ve yönetilmesini sağlar. MySQL, PostgreSQL, ve SQL Server gibi popüler veritabanı yönetim sistemleri, veritabanı tasarımı, sorgulama ve yönetim işlemlerini kolaylaştırır. Bu makalede, DBMS'lerin temel işlevlerini, avantajlarını ve yaygın kullanım alanlarını inceleyeceğiz. Ayrıca, ilişkisel ve NoSQL veritabanları arasındaki farkları da ele alacağız.",
                        "Veritabanı yönetim sistemleri üzerine temel bilgiler.", "037fd24802f9d193cdfa502f9de66e50.jpg", 8);
                    var Comment15 = new Comment("Veritabanı yönetimi hakkında çok şey öğrendim.", 8, 4);
                    var Comment16 = new Comment("SQL ve NoSQL veritabanları arasındaki farklar hakkında bilgi edinmek güzel oldu.", 8, 3);
                    Blog8.Comments.Add(Comment15);
                    Blog8.Comments.Add(Comment16);
                    Blog8.Tags.AddRange(Tags.Skip(3).Take(2).ToList());
                    Blog8.LikeCount = 11;

                    context.Blogs.Add(Blog8);
                    context.Comments.AddRange(Comment15, Comment16);
                    context.SaveChanges();

                    var Blog9 = new Blog("Web Güvenliği Temelleri",
                        "Web güvenliği, internet üzerindeki verilerin ve uygulamaların korunması açısından kritik bir öneme sahiptir. SQL enjeksiyonu, XSS saldırıları ve DDoS saldırıları gibi yaygın güvenlik tehditleri, web uygulamalarını hedef alabilir. Bu makalede, web güvenliğinin temel kavramlarını ve bu tehditlere karşı alınabilecek önlemleri inceleyeceğiz. Ayrıca, güvenli bir web uygulaması geliştirmek için izlenmesi gereken en iyi uygulamalara da değineceğiz.",
                        "Web güvenliği hakkında temel bilgiler.", "536d53c303372028262f2c4dce22fb5d.jpg", 9);
                    var Comment17 = new Comment("Web güvenliği hakkında farkındalığım arttı.", 9, 2);
                    var Comment18 = new Comment("Web uygulamaları geliştirirken güvenliği nasıl sağlayabileceğimi öğrendim.", 9, 1);
                    Blog9.Comments.Add(Comment17);
                    Blog9.Comments.Add(Comment18);
                    Blog9.Tags.AddRange(Tags.Take(2).ToList());
                    Blog9.LikeCount = 12;

                    context.Blogs.Add(Blog9);
                    context.Comments.AddRange(Comment17, Comment18);
                    context.SaveChanges();

                    var Blog10 = new Blog("React ile Modern Web Uygulamaları",
                        "React, Facebook tarafından geliştirilen popüler bir JavaScript kütüphanesidir. Tek sayfalı uygulamalar (SPA) geliştirmek için kullanılan React, bileşen tabanlı mimarisi sayesinde kod tekrarını azaltır ve geliştiricilere esneklik sağlar. Bu makalede, React'in temel prensiplerini, bileşen yapısını ve durum yönetimini inceleyeceğiz. Ayrıca, React ile kullanıcı arayüzleri geliştirmenin avantajlarına da değineceğiz.",
                        "React ile modern web uygulamaları geliştirme.", "597b85b85b300c154710d4e58760b654.jpg", 8);
                    var Comment19 = new Comment("React ile ilgili çok faydalı bilgiler öğrendim.", 10, 9);
                    var Comment20 = new Comment("Tek sayfalı uygulamalar için React kullanmayı düşüneceğim.", 10, 7);
                    Blog10.Comments.Add(Comment19);
                    Blog10.Comments.Add(Comment20);
                    Blog10.Tags.AddRange(Tags.Skip(2).Take(3).ToList());
                    Blog10.LikeCount = 13;

                    context.Blogs.Add(Blog10);
                    context.Comments.AddRange(Comment19, Comment20);
                    context.SaveChanges();

                    var Blog11 = new Blog("Kotlin ile Android Uygulama Geliştirme",
                        "Kotlin, JetBrains tarafından geliştirilen ve Android geliştirme için resmi olarak desteklenen modern bir programlama dilidir. Java'ya kıyasla daha basit ve daha güvenli bir dil olan Kotlin, Android geliştiricileri arasında hızla popülerlik kazandı. Kotlin'in sunduğu güçlü özellikler, kodun daha okunabilir ve sürdürülebilir olmasını sağlar. Bu makalede, Kotlin'in temel özelliklerini ve Android uygulama geliştirmede nasıl kullanıldığını inceleyeceğiz.",
                        "Kotlin ile Android uygulama geliştirme hakkında bilgiler.", "41230590a0ff35dd78f8856bd993affb.jpg", 7);
                    var Comment21 = new Comment("Kotlin öğrenmek için güzel bir rehber.", 11, 8);
                    var Comment22 = new Comment("Android uygulama geliştirme konusunda yeni bilgiler edindim.", 11, 3);
                    Blog11.Comments.Add(Comment21);
                    Blog11.Comments.Add(Comment22);
                    Blog11.Tags.AddRange(Tags.Skip(1).Take(3).ToList());
                    Blog11.LikeCount = 14;

                    context.Blogs.Add(Blog11);
                    context.Comments.AddRange(Comment21, Comment22);
                    context.SaveChanges();

                    var Blog12 = new Blog("Docker ile Uygulama Kapsülleme",
                        "Docker, uygulamaları ve bağımlılıklarını izole eden konteynerler içinde çalıştıran açık kaynaklı bir platformdur. Docker, geliştirme ve üretim ortamları arasında tutarlılığı sağlamak için kullanılır. Bu makalede, Docker'ın temel kavramlarını, avantajlarını ve kullanım senaryolarını inceleyeceğiz. Ayrıca, Docker ile konteyner oluşturma, yönetme ve dağıtma süreçlerine de değineceğiz.",
                        "Docker ile uygulama kapsülleme ve konteyner yönetimi.", "6245386771a1f92714f9f2bce7afbf4c.jpg", 6);
                    var Comment23 = new Comment("Docker hakkında bilmediğim birçok şeyi öğrendim.", 12, 7);
                    var Comment24 = new Comment("Kapsülleme ve konteyner yönetimi konusunda bilgilendirici bir yazı.", 12, 2);
                    Blog12.Comments.Add(Comment23);
                    Blog12.Comments.Add(Comment24);
                    Blog12.Tags.AddRange(Tags.Skip(2).Take(2).ToList());
                    Blog12.LikeCount = 15;

                    context.Blogs.Add(Blog12);
                    context.Comments.AddRange(Comment23, Comment24);
                    context.SaveChanges();

                    var Blog13 = new Blog("Machine Learning ile Öngörü Modelleri",
                        "Makine öğrenmesi, büyük veri kümeleri üzerinde öğrenme algoritmaları kullanarak öngörülerde bulunma sürecidir. Makine öğrenmesi modelleri, veri bilimi ve yapay zeka uygulamalarında yaygın olarak kullanılır. Bu makalede, makine öğrenmesi modellerinin nasıl çalıştığını, hangi algoritmaların kullanıldığını ve bu modellerin nasıl oluşturulup değerlendirildiğini inceleyeceğiz.",
                        "Machine Learning ile öngörü modelleri geliştirme.", "a2941f55f947539c07903d308a1b4fe2.jpg", 5);
                    var Comment25 = new Comment("Makine öğrenmesi hakkında çok faydalı bilgiler öğrendim.", 13, 9);
                    var Comment26 = new Comment("Öngörü modelleri konusunda daha fazla bilgi edinmek istiyorum.", 13, 4);
                    Blog13.Comments.Add(Comment25);
                    Blog13.Comments.Add(Comment26);
                    Blog13.Tags.AddRange(Tags.Take(2).ToList());
                    Blog13.LikeCount = 16;

                    context.Blogs.Add(Blog13);
                    context.Comments.AddRange(Comment25, Comment26);
                    context.SaveChanges();

                    var Blog14 = new Blog("Blockchain Teknolojisi ve Kripto Paralar",
                        "Blockchain, merkezi olmayan ve dağıtık bir veri tabanı teknolojisidir. Kripto paraların temelini oluşturan blockchain, güvenli ve şeffaf veri transferini sağlar. Bu makalede, blockchain teknolojisinin nasıl çalıştığını, kripto para birimlerinin bu teknolojiyle nasıl ilişkili olduğunu ve bu alandaki potansiyel uygulamaları inceleyeceğiz.",
                        "Blockchain ve kripto paralar üzerine genel bilgiler.", "b199e33e78993da6833dd05099c1b17b.jpg", 4);
                    var Comment27 = new Comment("Blockchain teknolojisi hakkında bilgiler çok faydalı oldu.", 14, 5);
                    var Comment28 = new Comment("Kripto paraların nasıl çalıştığını öğrenmek güzeldi.", 14, 3);
                    Blog14.Comments.Add(Comment27);
                    Blog14.Comments.Add(Comment28);
                    Blog14.Tags.AddRange(Tags.Skip(3).Take(3).ToList());
                    Blog14.LikeCount = 17;

                    context.Blogs.Add(Blog14);
                    context.Comments.AddRange(Comment27, Comment28);
                    context.SaveChanges();

                    var Blog15 = new Blog("Yapay Sinir Ağları ve Derin Öğrenme",
                        "Yapay sinir ağları, insan beyninin çalışma prensiplerini taklit eden yapay yapılar olarak tanımlanır. Derin öğrenme, yapay sinir ağları kullanılarak gerçekleştirilen bir makine öğrenmesi alt dalıdır. Bu makalede, yapay sinir ağlarının ve derin öğrenmenin temel kavramlarını, uygulama alanlarını ve gelecekteki potansiyelini inceleyeceğiz.",
                        "Yapay sinir ağları ve derin öğrenme üzerine bilgiler.", "bd1f367b7228cddf375fdb704c277a17.jpg", 3);
                    var Comment29 = new Comment("Yapay sinir ağları hakkında çok şey öğrendim.", 15, 8);
                    var Comment30 = new Comment("Derin öğrenme konusuna ilgi duymaya başladım.", 15, 6);
                    Blog15.Comments.Add(Comment29);
                    Blog15.Comments.Add(Comment30);
                    Blog15.Tags.AddRange(Tags.Take(3).ToList());
                    Blog15.LikeCount = 18;

                    context.Blogs.Add(Blog15);
                    context.Comments.AddRange(Comment29, Comment30);
                    context.SaveChanges();

                    var Blog16 = new Blog("SEO Optimizasyonu ve Web Trafiği Artırma",
                        "Arama motoru optimizasyonu (SEO), web sitelerinin arama motorlarında daha üst sıralarda yer almasını sağlamak için yapılan çalışmaların bütünüdür. SEO, web trafiğini artırmak için kritik bir rol oynar. Bu makalede, SEO'nun temel prensiplerini, kullanılan teknikleri ve web sitenizin görünürlüğünü artırmanın yollarını ele alacağız.",
                        "SEO optimizasyonu ve web trafiği artırma üzerine bilgiler.", "ca7eba29502c29097df191fcfe82325c.jpg", 2);
                    var Comment31 = new Comment("SEO konusunda çok faydalı bilgiler öğrendim.", 16, 1);
                    var Comment32 = new Comment("Web sitemin trafiğini nasıl artırabileceğimi öğrendim.", 16, 10);
                    Blog16.Comments.Add(Comment31);
                    Blog16.Comments.Add(Comment32);
                    Blog16.Tags.AddRange(Tags.Skip(1).Take(2).ToList());
                    Blog16.LikeCount = 19;

                    context.Blogs.Add(Blog16);
                    context.Comments.AddRange(Comment31, Comment32);
                    context.SaveChanges();

                    var Blog17 = new Blog("Web Tasarımında UI/UX Prensipleri",
                        "Kullanıcı arayüzü (UI) ve kullanıcı deneyimi (UX), web tasarımının temel bileşenleridir. İyi bir UI/UX tasarımı, kullanıcıların siteyle etkileşimini olumlu yönde etkiler. Bu makalede, UI ve UX tasarımının temel prensiplerini, iyi bir kullanıcı deneyimi sağlamak için yapılması gerekenleri ve web tasarımında dikkat edilmesi gereken unsurları inceleyeceğiz.",
                        "Web tasarımında UI/UX prensipleri ve ipuçları.", "cb25a2f08259499975fb151929d90908.jpg", 1);
                    var Comment33 = new Comment("UI/UX tasarımı hakkında çok şey öğrendim.", 17, 9);
                    var Comment34 = new Comment("Web sitemin kullanıcı deneyimini nasıl iyileştirebileceğimi öğrendim.", 17, 8);
                    Blog17.Comments.Add(Comment33);
                    Blog17.Comments.Add(Comment34);
                    Blog17.Tags.AddRange(Tags.Skip(2).Take(3).ToList());
                    Blog17.LikeCount = 20;

                    context.Blogs.Add(Blog17);
                    context.Comments.AddRange(Comment33, Comment34);
                    context.SaveChanges();

                    var Blog18 = new Blog("Python ile Web Geliştirme: Django ve Flask",
                        "Python, web geliştirme için de güçlü bir programlama dilidir. Django ve Flask, Python ile web geliştirme yaparken sıkça kullanılan iki popüler framework'tür. Django, daha çok 'bataryalar dahil' yaklaşımıyla bilinirken, Flask, minimalist ve esnek bir yapı sunar. Bu makalede, Django ve Flask'ın temel özelliklerini, avantajlarını ve hangi senaryolarda kullanılmalarının uygun olduğunu inceleyeceğiz.",
                        "Python ile web geliştirme: Django ve Flask karşılaştırması.", "cc4d1d1ec11b16435ee71bbf7a349c42.jpg", 2);
                    var Comment35 = new Comment("Django ve Flask arasındaki farkları öğrenmek güzeldi.", 18, 7);
                    var Comment36 = new Comment("Python ile web geliştirme konusunda daha fazla bilgi edindim.", 18, 6);
                    Blog18.Comments.Add(Comment35);
                    Blog18.Comments.Add(Comment36);
                    Blog18.Tags.AddRange(Tags.Take(2).ToList());
                    Blog18.LikeCount = 21;

                    context.Blogs.Add(Blog18);
                    context.Comments.AddRange(Comment35, Comment36);
                    context.SaveChanges();

                    var Blog19 = new Blog("Siber Güvenlik Temelleri ve Uygulama Yöntemleri",
                        "Siber güvenlik, dijital verilerin, ağların ve sistemlerin korunmasını sağlar. Veri ihlalleri, kimlik avı saldırıları ve kötü amaçlı yazılımlar gibi tehditlere karşı koruma sağlamak, bireyler ve kuruluşlar için hayati öneme sahiptir. Bu makalede, siber güvenlik temel prensiplerini, yaygın güvenlik açıklarını ve bu açıkları kapatmak için alınması gereken önlemleri inceleyeceğiz.",
                        "Siber güvenlik temelleri ve pratik uygulama yöntemleri.", "ee3369f79097a1e4c1c95742b2483a15.jpg", 3);
                    var Comment37 = new Comment("Siber güvenlik konusunda bilinçlenmek çok önemli.", 19, 5);
                    var Comment38 = new Comment("Veri güvenliğini nasıl sağlayabileceğimi öğrendim.", 19, 4);
                    Blog19.Comments.Add(Comment37);
                    Blog19.Comments.Add(Comment38);
                    Blog19.Tags.AddRange(Tags.Skip(3).Take(3).ToList());
                    Blog19.LikeCount = 22;

                    context.Blogs.Add(Blog19);
                    context.Comments.AddRange(Comment37, Comment38);
                    context.SaveChanges();

                    var Blog20 = new Blog("Agile ve Scrum: Yazılım Geliştirme Metodolojileri",
                        "Agile ve Scrum, yazılım geliştirme süreçlerini optimize etmek için kullanılan popüler metodolojilerdir. Agile, esnek ve tekrarlı bir süreç sunarken, Scrum, belirli roller ve ritüeller içeren bir framework sağlar. Bu makalede, Agile ve Scrum'ın temel prensiplerini, avantajlarını ve yazılım projelerinde nasıl uygulandıklarını inceleyeceğiz.",
                        "Agile ve Scrum metodolojileri üzerine bilgiler.", "335b856edafda79cba62e6e5439147bb.jpg", 4);
                    var Comment39 = new Comment("Agile ve Scrum'ı daha iyi anlamamı sağladı.", 20, 3);
                    var Comment40 = new Comment("Yazılım projelerinde bu metodolojileri nasıl uygulayabileceğimi öğrendim.", 20, 2);
                    Blog20.Comments.Add(Comment39);
                    Blog20.Comments.Add(Comment40);
                    Blog20.Tags.AddRange(Tags.Take(3).ToList());
                    Blog20.LikeCount = 23;

                    context.Blogs.Add(Blog20);
                    context.Comments.AddRange(Comment39, Comment40);
                    context.SaveChanges();

                    var Blog21 = new Blog("Vue.js ile Tek Sayfalı Uygulama Geliştirme",
                        "Vue.js, progresif bir JavaScript framework'üdür ve tek sayfalı uygulamalar (SPA) geliştirmek için kullanılır. Vue.js, esnek yapısı ve kapsamlı dokümantasyonu ile dikkat çeker. Bu makalede, Vue.js'in temel kavramlarını, bileşen yapısını ve durum yönetimini inceleyeceğiz. Ayrıca, Vue.js ile SPA geliştirme konusunda ipuçları ve en iyi uygulamaları paylaşacağız.",
                        "Vue.js ile tek sayfalı uygulama geliştirme rehberi.", "3d56f65a800620775c61c04282edd0fc.jpg", 5);
                    var Comment41 = new Comment("Vue.js öğrenmek isteyenler için harika bir kaynak.", 21, 1);
                    var Comment42 = new Comment("SPA geliştirme konusunda Vue.js kullanmayı deneyeceğim.", 21, 10);
                    Blog21.Comments.Add(Comment41);
                    Blog21.Comments.Add(Comment42);
                    Blog21.Tags.AddRange(Tags.Skip(1).Take(2).ToList());
                    Blog21.LikeCount = 24;

                    context.Blogs.Add(Blog21);
                    context.Comments.AddRange(Comment41, Comment42);
                    context.SaveChanges();

                    var Blog22 = new Blog("Mobil Uygulama Geliştirme: Swift vs Kotlin",
                        "Mobil uygulama geliştirme dünyasında Swift ve Kotlin, iOS ve Android platformları için popüler programlama dilleridir. Swift, Apple tarafından geliştirilmiş ve iOS uygulamaları için optimize edilmiştir. Kotlin ise, Android uygulama geliştirme için Java'ya modern bir alternatif sunar. Bu makalede, Swift ve Kotlin'in özelliklerini, avantajlarını ve hangi senaryolarda kullanılmalarının uygun olduğunu inceleyeceğiz.",
                        "Mobil uygulama geliştirme: Swift ve Kotlin karşılaştırması.", "60d45d1243eec4c28fbf821b9e8474f8.jpg", 6);
                    var Comment43 = new Comment("Swift ve Kotlin arasındaki farkları öğrenmek çok faydalı oldu.", 22, 9);
                    var Comment44 = new Comment("Hangi dilde mobil uygulama geliştireceğime karar vermemde yardımcı oldu.", 22, 8);
                    Blog22.Comments.Add(Comment43);
                    Blog22.Comments.Add(Comment44);
                    Blog22.Tags.AddRange(Tags.Skip(2).Take(3).ToList());
                    Blog22.LikeCount = 25;

                    context.Blogs.Add(Blog22);
                    context.Comments.AddRange(Comment43, Comment44);
                    context.SaveChanges();

                    var Blog23 = new Blog("Big Data ve Veri Analitiği",
                        "Büyük veri (Big Data), geleneksel veri işleme yöntemleriyle işlenemeyecek kadar büyük, hızlı ve çeşitli veri kümelerini ifade eder. Veri analitiği, bu büyük veri kümelerinden anlamlı bilgiler çıkarma sürecidir. Bu makalede, büyük veri kavramını, veri analitiği tekniklerini ve bu tekniklerin iş dünyasında nasıl kullanıldığını inceleyeceğiz.",
                        "Big Data ve veri analitiği üzerine temel bilgiler.", "bf2198626d0c9e041d0b36f8ac0cb55b.jpg", 7);
                    var Comment45 = new Comment("Büyük veri ve veri analitiği konularında çok şey öğrendim.", 23, 7);
                    var Comment46 = new Comment("Veri analitiği tekniklerini uygulamak istiyorum.", 23, 6);
                    Blog23.Comments.Add(Comment45);
                    Blog23.Comments.Add(Comment46);
                    Blog23.Tags.AddRange(Tags.Take(2).ToList());
                    Blog23.LikeCount = 26;

                    context.Blogs.Add(Blog23);
                    context.Comments.AddRange(Comment45, Comment46);
                    context.SaveChanges();

                    var Blog24 = new Blog("Bulut Bilişim ve AWS ile Çözümler",
                        "Bulut bilişim, bilgi işlem kaynaklarının internet üzerinden sağlanmasıdır. Amazon Web Services (AWS), dünya çapında yaygın olarak kullanılan bir bulut bilişim platformudur. AWS, geniş bir hizmet yelpazesi sunar ve kullanıcıların altyapılarını ölçeklendirmelerine olanak tanır. Bu makalede, bulut bilişim kavramını, AWS'nin sunduğu hizmetleri ve bulut çözümlerinin avantajlarını inceleyeceğiz.",
                        "Bulut bilişim ve AWS çözümleri üzerine bilgiler.", "373039f98220bbba602a9514309ba1e6.jpg", 8);
                    var Comment47 = new Comment("AWS kullanarak bulut çözümleri geliştirme hakkında bilgiler çok faydalı oldu.", 24, 5);
                    var Comment48 = new Comment("Bulut bilişim konusunda daha fazla bilgi edinmek istiyorum.", 24, 4);
                    Blog24.Comments.Add(Comment47);
                    Blog24.Comments.Add(Comment48);
                    Blog24.Tags.AddRange(Tags.Skip(1).Take(3).ToList());
                    Blog24.LikeCount = 27;

                    context.Blogs.Add(Blog24);
                    context.Comments.AddRange(Comment47, Comment48);
                    context.SaveChanges();

                    var Blog25 = new Blog("Veri Bilimi ve Yapay Zeka Uygulamaları",
                        "Veri bilimi, yapılandırılmış ve yapılandırılmamış verilerden anlamlı bilgiler çıkarma sürecidir. Yapay zeka (AI) ise, makinelerin insan benzeri zekaya sahip olduğu sistemlerin geliştirilmesini ifade eder. Bu makalede, veri biliminin temel kavramlarını, yapay zeka uygulamalarını ve bu iki alanın nasıl bir araya geldiğini inceleyeceğiz.",
                        "Veri bilimi ve yapay zeka uygulamaları üzerine genel bilgiler.", "bd92e9542100a24550ab49c12e884789.jpg", 9);
                    var Comment49 = new Comment("Veri bilimi ve yapay zeka hakkında çok şey öğrendim.", 25, 3);
                    var Comment50 = new Comment("Bu alanlarda kariyer yapmayı düşünüyorum.", 25, 2);
                    Blog25.Comments.Add(Comment49);
                    Blog25.Comments.Add(Comment50);
                    Blog25.Tags.AddRange(Tags.Take(2).ToList());
                    Blog25.LikeCount = 28;

                    context.Blogs.Add(Blog25);
                    context.Comments.AddRange(Comment49, Comment50);
                    context.SaveChanges();

                    var Blog26 = new Blog("Node.js ile API Geliştirme",
                        "Node.js, asenkron ve olay odaklı yapısıyla yüksek performanslı ve ölçeklenebilir sunucu uygulamaları geliştirmeyi mümkün kılar. JavaScript tabanlı bu platform, geliştiricilere sunucu tarafında da aynı dili kullanma avantajı sağlar. Express.js gibi popüler framework'lerle birleştiğinde, RESTful API'ler oluşturmak çok daha kolay hale gelir. Bu yazıda, Node.js ve Express.js ile API geliştirme süreçlerini ele alacağız.",
                        "Node.js ile yüksek performanslı API geliştirme.", "410c7795403a6b61bc468db21b9b443b.jpg", 10);
                    var Comment51 = new Comment("Node.js ile ilgili bilmediğim çok şey varmış.", 26, 6);
                    var Comment52 = new Comment("API geliştirme konusunda yol gösterici bir yazı.", 26, 1);
                    Blog26.Comments.Add(Comment51);
                    Blog26.Comments.Add(Comment52);
                    Blog26.Tags.AddRange(Tags.Take(3).ToList());
                    Blog26.LikeCount = 20;

                    context.Blogs.Add(Blog26);
                    context.Comments.AddRange(Comment51, Comment52);
                    context.SaveChanges();
                }
            }
        }
    }
}
