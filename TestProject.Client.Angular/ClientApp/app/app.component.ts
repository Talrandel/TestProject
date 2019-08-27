import { Component, OnInit } from '@angular/core';
import { Person } from './person'
import { PersonService } from './person.Service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [PersonService]
})
export class AppComponent implements OnInit {

    selectedPerson: Person = new Person();    // изменяемый товар
    persons: Person[];                  // массив товаров
    tableMode: boolean = true;          // табличный режим

    constructor(private dataService: PersonService) { }

    ngOnInit() {
        this.loadProducts();    // загрузка данных при старте компонента  
    }
    // получаем данные через сервис
    loadProducts() {
        this.dataService.getPersons()
            .subscribe((data: Person[]) => this.persons = data);
    }

}