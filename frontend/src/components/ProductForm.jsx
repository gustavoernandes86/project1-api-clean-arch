import React, { useState } from "react";

const API_BASE = "http://localhost:5000/api/products";

export default function ProductForm() {
  const [name, setName] = useState("");
  const [price, setPrice] = useState("");
  const [description, setDescription] = useState("");
  const [loading, setLoading] = useState(false);

  const submit = async (e) => {
    e?.preventDefault();
    if (!name || !price) {
      alert("Nome e preço são obrigatórios");
      return;
    }
    const payload = {
      name: name.trim(),
      price: Number(price),
      description: description.trim() || null
    };
    setLoading(true);
    try {
      const res = await fetch(API_BASE, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
      });
      if (res.status === 201 || res.ok) {
        const created = await res.json().catch(()=>null);
        alert("Criado com sucesso");
        // limpa formulário
        setName(""); setPrice(""); setDescription("");
        // opcional: disparar evento custom para recarregar lista
        window.dispatchEvent(new Event("product:created"));
      } else {
        const body = await res.json().catch(()=>({}));
        alert("Erro: " + (body.title || res.status));
      }
    } catch (err) {
      alert("Erro: " + err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <form className="product-form card" onSubmit={submit}>
      <h2>Criar Produto</h2>

      <label>
        Nome
        <input value={name} onChange={e => setName(e.target.value)} placeholder="Nome do produto" />
      </label>

      <label>
        Preço
        <input value={price} onChange={e => setPrice(e.target.value)} placeholder="0.00" />
      </label>

      <label>
        Descrição
        <textarea value={description} onChange={e => setDescription(e.target.value)} placeholder="Descrição (opcional)"></textarea>
      </label>

      <div className="form-actions">
        <button type="submit" disabled={loading}>{loading ? "Salvando..." : "Salvar"}</button>
      </div>
    </form>
  );
}
