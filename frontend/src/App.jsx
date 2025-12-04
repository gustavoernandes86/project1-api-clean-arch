import React from "react";
import ProductList from "./components/ProductList";
import ProductForm from "./components/ProductForm";

export default function App() {
  return (
    <div className="app">
      <header>
        <h1>Project1 - Frontend de Teste</h1>
        <p>Frontend simples para testar a API de Produtos</p>
      </header>

      <main>
        <section className="left">
          <ProductForm />
        </section>
        <section className="right">
          <ProductList />
        </section>
      </main>

      <footer>
        <small>Frontend de teste — conecta em http://localhost:5000</small>
      </footer>
    </div>
  );
}
