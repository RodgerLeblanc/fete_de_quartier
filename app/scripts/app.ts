function popup(id: number) {
    const url = 'Events/Details/' + id.toString();
    const name = 'Détails';
    const options = 'height=500,width=400,status=yes,toolbar=no,scrollbars=yes';

    window.open(url, name, options)
}