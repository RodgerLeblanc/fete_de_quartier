function popup(id: number) {
    const url = 'Events/Details/' + id.toString();
    const name = 'Détails';
    const options = 'height=500,width=400,status=yes,toolbar=no,scrollbars=yes';

    window.open(url, name, options)
}

interface EventViewModel {
    Id: number;
    Name: string;
    Description: string;
    Start: string;
    End: string;
    LocationName: string;
    LocationAddress: string;
}

document.querySelectorAll<HTMLTableRowElement>(".clickable-row")
    .forEach((row) => {

        row.addEventListener("click", () => {

            const jsonString = row.dataset.object;

            if (!jsonString) {
                console.error("Aucune donnée JSON trouvée");
                return;
            }

            let eventViewModel: EventViewModel;

            try {
                eventViewModel = JSON.parse(jsonString);
            } catch (e) {
                console.error("Erreur parsing JSON", e);
                return;
            }

            // Mets le nom de l'évènement comme titre de la page modale
            const titleElement = document.getElementById("detailsModalTitle");

            if (titleElement) {
                titleElement.textContent = eventViewModel.Name;
            }

            const list = document.getElementById("detailsList") as HTMLUListElement;
            list.innerHTML = "";

            (Object.keys(eventViewModel) as Array<keyof Event>).forEach((key) => {
                const li = document.createElement("li");

                const value = eventViewModel[key];
                li.textContent = `${key}: ${value}`;

                list.appendChild(li);
            });

            const modalElement = document.getElementById("detailsModal");

            if (!modalElement) {
                console.error("Modal introuvable");
                return;
            }

            const modal = new (window as any).bootstrap.Modal(modalElement);
            modal.show();
        });

    });
