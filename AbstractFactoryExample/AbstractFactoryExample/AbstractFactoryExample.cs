using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryExample
{
    interface IPayment
    {
        void Pay();
    }

    interface IRefund
    {
        void Refund();
    }

    interface IInvoice
    {
        void Invoice();
    }

    class PayPalPayment : IPayment
    {
        public void Pay()
        {
            Console.WriteLine("Processing PayPal payment...");
        }
    }

    class PayPalRefund : IRefund
    {
        public void Refund()
        {
            Console.WriteLine("Processing PayPal refund...");
        }
    }

    class PayPalInvoice : IInvoice
    {
        public void Invoice()
        {
            Console.WriteLine("Generating PayPal invoice...");
        }
    }

    class RazorpayPayment : IPayment
    {
        public void Pay()
        {
            Console.WriteLine("Processing Razorpay payment...");
        }
    }

    class RazorpayRefund : IRefund
    {
        public void Refund()
        {
            Console.WriteLine("Processing Razorpay refund...");
        }
    }

    class RazorpayInvoice : IInvoice
    {
        public void Invoice()
        {
            Console.WriteLine("Generating Razorpay invoice...");
        }
    }

    interface IPaymentFactory
    {
        IPayment CreatePayment();
        IRefund CreateRefund();
        IInvoice CreateInvoice();
    }

    class PayPalFactory : IPaymentFactory
    {
        public IPayment CreatePayment()
        {
            return new PayPalPayment();
        }
        public IRefund CreateRefund()
        {
            return new PayPalRefund();
        }
        public IInvoice CreateInvoice()
        {
            return new PayPalInvoice();
        }
    }

    class RazorpayFactory : IPaymentFactory
    {
        public IPayment CreatePayment()
        {
            return new RazorpayPayment();
        }
        public IRefund CreateRefund()
        {
            return new RazorpayRefund();
        }
        public IInvoice CreateInvoice()
        {
            return new RazorpayInvoice();
        }
    }

    class PaymentProcessor
    {
        private IPaymentFactory _paymentFactory;
        public PaymentProcessor(IPaymentFactory paymentFactory)
        {
            _paymentFactory = paymentFactory;
        }
        public void ProcessPayment()
        {
            var payment = _paymentFactory.CreatePayment();
            payment.Pay();
        }
        public void ProcessRefund()
        {
            var refund = _paymentFactory.CreateRefund();
            refund.Refund();
        }
        public void GenerateInvoice()
        {
            var invoice = _paymentFactory.CreateInvoice();
            invoice.Invoice();
        }

        public void togglePaymentProcessor(IPaymentFactory paymentFactory)
        {
            _paymentFactory = paymentFactory;
        }

        public void ProcessAll()
        {
            ProcessPayment();
            ProcessRefund();
            GenerateInvoice();
        }
    }

    internal class AbstractFactoryExample
    {
        static void Main(string[] args)
        {
            PaymentProcessor payProcessor = new PaymentProcessor(new PayPalFactory());
            payProcessor.ProcessAll();
            payProcessor.togglePaymentProcessor(new RazorpayFactory());
            payProcessor.ProcessAll();
        }
    }
}
