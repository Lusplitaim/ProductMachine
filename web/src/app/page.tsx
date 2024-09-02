'use client'

import { createBrowserRouter, Outlet, RouterProvider } from "react-router-dom";
import Products from "./components/products/Products";
import Basket from "./components/basket/Basket";

export default function Home() {
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
      ],
    },
  ]);
  
  return (
    <RouterProvider router={router} />
  );
}
