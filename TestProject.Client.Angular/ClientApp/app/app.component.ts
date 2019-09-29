import { Component, OnInit } from '@angular/core';
import { Person } from './person'
import { PersonService } from './person.Service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [PersonService]
})
export class AppComponent implements OnInit {

    selectedPerson: Person = new Person();
    persons: Person[];
    tableMode: boolean = true;

    constructor(private dataService: PersonService) { }

    ngOnInit() {
        this.loadProducts();
    }

    loadProducts() {
        this.dataService.getPersons()
            .subscribe((data: Person[]) => this.persons = data);
    }

}