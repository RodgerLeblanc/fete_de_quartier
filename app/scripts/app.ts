function popup(id: number) {
    const url = 'Events/Details/' + id.toString();
    const name = 'Détails';
    const options = 'height=500,width=400,status=yes,toolbar=no,scrollbars=yes';

    window.open(url, name, options)
}

// Équivalent à la classe C# EventViewModel
interface EventViewModel {
    Id: number;
    Name: string;
    Description: string;
    Start: string;
    End: string;
    LocationName: string;
    LocationAddress: string;
}

// Pour chaque ligne de tableau possédant la classe "clickable-row" ...
document.querySelectorAll<HTMLTableRowElement>(".clickable-row")
    .forEach((row) => {

        // Gère cliquage
        row.addEventListener("click", () => {

            // Récupère la string JSON représentant l'objet de la ligne
            const jsonString = row.dataset.object;

            if (!jsonString) {
                console.error("Aucune donnée JSON trouvée");
                return;
            }

            // Peuple l'objet EventViewModel avec l'info du JSON
            let eventViewModel: EventViewModel;

            try {
                eventViewModel = JSON.parse(jsonString);
            } catch (e) {
                console.error("Erreur parsing JSON", e);
                return;
            }

            // Remplis la page modale
            var element = document.getElementById("detailsTitle");
            if (element) {
                element.textContent = eventViewModel.Name;
            }

            element = document.getElementById("detailsLocation");
            if (element) {
                element.innerHTML = `<b>Lieu</b> : ${eventViewModel.LocationName}`;

                if (eventViewModel.LocationAddress != null) {
                    element.innerHTML += ` - ${eventViewModel.LocationAddress}`;
                }
            }

            element = document.getElementById("detailsStart");
            if (element) {
                let date = new Date(eventViewModel.Start);

                element.innerHTML = `<b>Début</b> : ${date.toLocaleDateString() + " à " + date.toLocaleTimeString().split(" min ")[0]}`;
            }

            element = document.getElementById("detailsEnd");
            if (element) {
                let date = new Date(eventViewModel.End);

                element.innerHTML = `<b>Fin</b> : ${date.toLocaleDateString() + " à " + date.toLocaleTimeString().split(" min ")[0]}`;
            }

            element = document.getElementById("detailsDescription");
            if (element) {
                element.innerHTML = `<b>Description</b> : ${eventViewModel.Description}`;
            }

            // Sélectionne la modale
            const modalElement = document.getElementById("detailsModal");

            if (!modalElement) {
                console.error("Modal introuvable");
                return;
            }

            // Mets la modale en avant-plan
            modalElement.classList.add("show");
            document.body.classList.add("modal-open");
            modalElement.focus();

            // Cliquer en dehors ferme la modale
            modalElement.addEventListener("click", (e) => {
                if (e.target === modalElement) {
                    modalElement.classList.remove("show");
                    document.body.classList.remove("modal-open");
                }
            });

            // Peser sur escape le ferme la modale
            modalElement.addEventListener("keydown", (e) => {
                if (e.key === 'Escape') {
                    modalElement.classList.remove("show");
                    document.body.classList.remove("modal-open");
                }
            });

            // Affiche ma modale
            const modal = new (window as any).bootstrap.Modal(modalElement);

            modal.show();
        });
    });
