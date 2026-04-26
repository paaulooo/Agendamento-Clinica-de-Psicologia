fetch("/profissional")
    .then(res => {
        if (!res.ok) throw new Error(`Erro HTTP ${res.status}`);
        return res.json();
    })
    .then(data => {
        const select = document.getElementById("profissional");

        if (data.length === 0) {
            select.innerHTML = '<option disabled>Nenhum profissional cadastrado</option>';
            return;
        }

        data.forEach(p => {
            const option = document.createElement("option");
            option.value = p.id;
            option.textContent = p.nome;
            select.appendChild(option);
        });
    })
    .catch(err => {
        console.error("Erro ao carregar profissionais:", err);
        document.getElementById("profissional").innerHTML =
            `<option disabled>Erro: ${err.message}</option>`;
    });

document.getElementById("profissional").addEventListener("change", function () {
    const id = this.value;
    if (!id) return;

    fetch(`/agenda/${id}`)
        .then(res => {
            if (!res.ok) throw new Error(`Erro HTTP ${res.status}`);
            return res.json();
        })
        .then(data => montarAgenda(data))
        .catch(err => {
            console.error("Erro ao carregar agenda:", err);
            document.getElementById("agenda").innerHTML =
                `<tr><td colspan="7">Erro ao carregar agenda: ${err.message}</td></tr>`;
        });
});

function montarAgenda(dados) {
    const tabela = document.getElementById("agenda");
    const dias = ["Seg", "Ter", "Qua", "Quin", "Sex", "Sab"];

    let html = "<tr><th>Hora</th>";
    dias.forEach(d => html += `<th>${d}</th>`);
    html += "</tr>";

    for (let h = 7; h <= 21; h++) {
        html += `<tr><td>${h}h</td>`;

        dias.forEach(dia => {
            const item = dados.find(a => a.hora === h && a.diaSemana === dia);
            let cor = "white";
            let texto = "";

            if (item) {
                texto = item.status;
                if (texto === "Livre") cor = "lightgreen";
                else if (texto === "Bloqueado") cor = "gray";
                else cor = "yellow";
            }

            html += `<td style="background:${cor}">${texto}</td>`;
        });

        html += "</tr>";
    }

    tabela.innerHTML = html;
}
