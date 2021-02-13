using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SI_Master.Models;

namespace SI_Master.REST
{
    public interface Api
    {
        //TODO login
        [Post("/register_master")]
        Task<AuthData> Login([Query]DeviceRegistration registrationData);

        //⦁	2.1.1. Запрос «Рабочий стол»: 
        [Get("/dashboard")]
        Task<Answer> Getdashboard(AuthData authdata);

        [Get("/")]
        Task<HttpResponseMessage> CheckUserCredentials(AuthData authdata);

        //⦁	2.1.2. Запрос «Заказ ключей»
        [Post("/dashboard/order_keys")]
        Task<Answer> OrderKeys([Query]Dictionary<string, object> keysAmount, [Query]AuthData authdata);

        //⦁	2.1.3. Запрос «Код мобильного приложения»: 
        [Post("/dashboard/mobile_code")]
        Task<Answer> MobileAppCode([Query]AuthData authdata);

        ////⦁	2.1.4. Запрос «Рабочий стол: дополнительно»: 
        //[Get("/dashboard/misc")]
        //Task<Answer> GetDashBoardMisc(AuthData authdata);

        //⦁	2.2.1. Запрос «Карты» 
        [Get("/cards")]
        Task<Answer> Cards(Dictionary<string, object> filters, AuthData authdata);

        ////⦁	2.2.2. Запрос «Установка лимитов»: 
        [Post("/cards/limits")]
        Task<Answer> SendCardLimits([Query]Dictionary<string, string> CardLimits, [Query]AuthData authData);

        ////⦁	2.2.3. Запрос «Заявка на карты»: 
        //[Post("/cards/order")]
        //Task<Answer> CardOrder(Limits limits, AuthData authData);

        ////⦁	2.2.4. Запрос «Блокировка/разблокировка»: 
        [Post("/cards/block")]
        Task<Answer> LockOrUbnlockCards([Query]Dictionary<string, string> cardLock, [Query]AuthData authData);

        ////⦁	2.2.5. Запрос «Установка держателя»: 
        //[Post("/cards/set_holder")]
        //Task<Answer> SetCardHolder(CardHolder cardHolder, AuthData authData);

        //⦁	2.3. Запрос «Транзакции» (с пагинацией)
        [Get("/transactions")]
        Task<Answer> Transactions(Dictionary<string, object> filters, AuthData authData);

        //⦁	2.4. Запрос «Взаиморасчёты»: 
        [Get("/settlements")]
        Task<Answer> Settlements(Dictionary<string, object> filters, AuthData authData);

        ////⦁	2.5.1. Запрос «Документы: список» добавить в пункт документов - топливо
        //[Get("/documents")]
        //Task<Answer> Documents(Filters filters, AuthData authData);

        ////⦁	2.5.2. Запрос «Заказ счёта»: добавить в докумнеты - личный кабинет
        //[Post("/documents/order_bill")]
        //Task<Answer> DocumentsBill(Filters filters, AuthData authData);

        ////⦁	2.5.3. Запрос «Скачивание документа»: добавить в документы - личный кабинет
        //[Post("/documents/download")]
        //Task<HttpResponseMessage> DocumentsDownload(Filters filters, string id, AuthData authData);

        ////⦁	2.6. Запрос «Документы
        //[Get("/documents_si")]
        //Task<Answer> DocumentsSI(FilterCards filters, AuthData authData);

        ////⦁	2.7.1. Запрос «Запросы на (раз-)блокировку»
        [Get("/request_archive/cards/block")]
        Task<Answer> ARchiveCardBlock(Dictionary<string, object> filters, AuthData authData);

        //⦁	2.7.2. Запрос «Запросы на установку лимитов»
        [Get("/request_archive/cards/limits")]
        Task<Answer> ArchiveLimits(Dictionary<string, object> filters, AuthData authData);

        ////⦁	2.7.3. Запрос «Запросы на новые карты»
        [Get("/request_archive/cards/order")]
        Task<Answer> ArchiveCards(Dictionary<string, object> filters, AuthData authData);

        ////⦁	2.8. Запрос «Договоры»
        //[Get("/contracts")]
        //Task<Answer> Contracts(FilterCards filters, AuthData authData);

        ////⦁	2.9.1. Запрос «Договор обещанного платежа»
        //[Get("/promised_payment/download_contract")]
        //Task<Answer> PromisedPayment(FileInfoPart filters, AuthData authData);

        ////⦁	2.9.2. Запрос «Заявка на обещанный платеж»:
        //[Post("/promised_payment/apply ")]
        //Task<Answer> PromesidPaymentAplly(PromisedPayment paymentApply, AuthData authData);

        ////⦁	2.10. Запрос «Словарь лимитов»
        [Get("/dictionary/cards")]
        Task<Answer> DictionaryLimits(AuthData authData);

        [Post("/fcm_memorize")]
        Task<Answer> MemorizeFCMToken([Query]FCMAuthData authData);

        [Post("/fcm_forget")]
        Task<Answer> ForgetFCMToken([Query]FCMAuthData authData);


        [Post("/pos_nav/step0")]
        Task<Answer> GetQRCode([Query] AuthData authData, [Query] Geoposition geoposition);

        [Post("/pos_nav/check_visit")]
        Task<Answer> GetOrderState([Query] AuthData authdata, [Query] string visit_id);

        [Post("/pos_nav/accept/get_accepted_list")]
        Task<Answer> GetNegotiateWorks([Query] AuthData authdata, [Query] string visit_id);

    }
}
