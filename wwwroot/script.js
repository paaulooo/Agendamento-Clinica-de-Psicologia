const apiUrl = "https://localhost:7236";

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
        console.error("Erro ao carregar profissional:", err);
        document.getElementById("profissional").innerHTML =
            `<option disabled>Erro: ${err.message}</option>`;
    });

document.getElementById("profissional")
    .addEventListener("change", function () {

        console.log("mudou profissional");

        const profissionalId = this.value;

        fetch(`${apiUrl}/agenda/${profissionalId}`)
            .then(res => res.json())
            .then(data => montarAgenda(data));
    });

function montarAgenda(dados) {

    const tabela = document.getElementById("agenda");
    tabela.innerHTML = "";

    const dias = [
        "Segunda",
        "Terça",
        "Quarta",
        "Quinta",
        "Sexta",
        "Sábado"
    ];

    let header = "<tr><th>Hora</th>";

    dias.forEach(d => {
        header += `<th>${d}</th>`;
    });

    header += "</tr>";

    tabela.innerHTML += header;

    for (let h = 7; h <= 21; h++) {

        let linha = `<tr><td>${h}:00</td>`;

        dias.forEach(dia => {

            const item = dados.find(a =>
                a.hora === h &&
                a.diaSemana === dia
            );

            let texto = "";
            let cor = "white";

            if (item) {

                texto = item.status;

                switch (item.status) {

                    case "Atendimento":
                        cor = "#8ecae6";
                        break;

                    case "Online":
                        cor = "#90be6d";
                        break;

                    case "CLS":
                        cor = "#f9c74f";
                        break;

                    case "Indisponível":
                        cor = "#adb5bd";
                        break;

                    default:
                        cor = "#ffffff";
                        break;
                }
            }

            if (item) {
                texto = item.paciente;
            }

            linha += `
            <td style="
                background:${cor};
                text-align:center;
                font-weight:bold;
            ">
                ${texto}
            </td>`;

        });

        linha += "</tr>";

        tabela.innerHTML += linha;
    }
}
