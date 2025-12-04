import React, { useEffect, useState } from "react";

const API_BASE = "http://localhost:5000/api/products";

export default function ProductList() {
  const [items, setItems] = useState([]);
  const [loading, setLoading] = useState(false);
  const [selected, setSelected] = useState(null);
  const [error, setError] = useState("");

  const load = async () => {
    setLoading(true);
    try {
      const res = await fetch(API_BASE);
      if (!res.ok) throw new Error("Falha ao carregar produtos");
      const data = await res.json();
      setItems(data);
    } catch (err) {
      setError(String(err));
    } finally {
      setLoading(false);
    }
  };

useEffect(() => {
  load();
  const handle = () => load();
  window.addEventListener("product:created", handle);
  return () => window.removeEventListener("product:created", handle);
}, []);


  const remove = async (id) => {
    if (!confirm("Deseja realmente apagar este produto?")) return;
    try {
      const res = await fetch(`${API_BASE}/${id}`, { method: "DELETE" });
      if (res.status === 204) {
        setItems(items.filter(i => i.id !== id));
        if (selected?.id === id) setSelected(null);
      } else {
        const body = await res.json().catch(()=>({}));
        alert("Erro ao apagar: " + (body.title || res.status));
      }
    } catch (err) {
      alert("Erro: " + err);
    }
  };

  return (
    <div className="product-list card">
      <h2>Produtos</h2>
      {error && <div className="error">{error}</div>}
      {loading ? (
        <div>Carregando...</div>
      ) : (
        <>
          <ul>
            {items.map(p => (
              <li key={p.id} className="product-item">
                <div className="meta">
                  <strong>{p.name}</strong>
                  <span>R$ {Number(p.price).toFixed(2)}</span>
                </div>
                <div className="actions">
                  <button onClick={() => setSelected(p)}>Ver</button>
                  <button onClick={() => remove(p.id)} className="danger">Apagar</button>
                </div>
              </li>
            ))}
          </ul>

          {selected && (
            <div className="detail">
              <h3>Detalhes</h3>
              <p><strong>Nome:</strong> {selected.name}</p>
              <p><strong>Descrição:</strong> {selected.description || "-"}</p>
              <p><strong>Preço:</strong> R$ {Number(selected.price).toFixed(2)}</p>
              <button onClick={() => setSelected(null)}>Fechar</button>
            </div>
          )}
        </>
      )}
      <div className="refresh">
        <button onClick={load}>Atualizar</button>
      </div>
    </div>
  );
}
