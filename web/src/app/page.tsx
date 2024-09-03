'use client'

import { createBrowserRouter, Outlet, RouterProvider } from "react-router-dom";
import Products from "./components/products/Products";
import Basket from "./components/basket/Basket";
import Payment from "./components/payment/Payment";
import PaymentSuccess from "./components/payment-success/PaymentSuccess";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Outlet />,
    children: [
      {
        path: "",
        element: <Products />,
      },
      {
        path: "basket",
        element: <Basket />,
      },
      {
        path: "payment",
        element: <Payment />,
      },
      {
        path: "payment-success",
        element: <PaymentSuccess />,
      },
    ],
  },
]);

export default function Home() {
  return (
    <RouterProvider router={router} />
  );
}
