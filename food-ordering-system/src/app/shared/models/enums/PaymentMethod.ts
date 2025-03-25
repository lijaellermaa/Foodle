export enum PaymentMethod {
  Cash = 0,
  CreditCard = 1,
}

export function getPaymentMethodLabel(value: PaymentMethod): string {
  switch (value) {
    case PaymentMethod.Cash:
      return "Cash";
    case PaymentMethod.CreditCard:
      return "Credit Card";
  }
}
