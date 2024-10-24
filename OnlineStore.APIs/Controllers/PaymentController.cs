using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.APIs.Controllers;
using OnlineStore.APIs.Error;
using OnlineStore.Core.Entities.Basket_Module;
using OnlineStore.Core.Services.Contract.Payment;
using Stripe;


namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private const string endpointSecret = "whsec_46fd657505e08f6e64e1f12b8a000f3c759dc915425987c51ccd7fd5f1162d19";

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("{basketId}")]
        [Authorize]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketId);
            if (basketId is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                // Verify the event by checking the signature
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    endpointSecret
                );
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                // Handle the event
                if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                    // Handle successful payment intent
                    await _paymentService.UpdatePaymentIntentForSuccessedOrFailed(paymentIntent.Id, true);
                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                {
                    // Handle faild payment intent
                    await _paymentService.UpdatePaymentIntentForSuccessedOrFailed(paymentIntent.Id, false);
                }
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error processing webhook: {e.Message}");
                return BadRequest();
            }
        }
    }
}