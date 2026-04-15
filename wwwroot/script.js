// JavaScript source code
const apiUrl = "https://localhost:7236"; //sua API

fetch(`${apiUrl}/profissional`)
    .then(res => res.json())
    .then(data => { 
        console.log(data);

        const select = document.getElementById("profissional");

        data.forEach(p => {
            const option = document.createElement("option");
            option.value = p.id;
            option.textContent = p.nomeProfissional;
            select.appendChild(option);
        });
    });

document.getElementById("profissional").addEventListener("change", function() {
    const id = this.value;

    fetch(`${apiUrl}/agenda/${id}`)
    then(res => res.json())
        .then(data => montarAgenda(data));
});

function montarAgenda(dados) {
    const tabela = document.getElementById("agenda");
    tabela.innerHTML = "";

    const dias = ["Seg", "Ter", "Qua", "Quin", "Sex", "Sab"];
        
    let header = "<tr><th>Hora</th>";
    dias.forEach(d => header += `<th>${d}</th>`);
    header += "</tr>";

    tabela.innerHTML = header;

    for (let h = 7; h <= 21; h++) {
        let linha = `<tr><td>${h}h</td>`;

        dias.forEach(dia => {
            const item = dados.find(a => a.hora == h && a.diaSemana == dia);

            let cor = "white";
            let texto = "";

            if (item) {
                texto = item.status;

                if (texto === "Livre") cor = "lightgreen";
                else if (texto === "Bloqueado") cor = "gray";
                else cor = "yellow";
            }

            linha += `<td style="background:${cor}">${texto}</td>`;
        });

        linha += "</tr>";
        tabela.innerHTML += linha;
    }
} 