import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ConsultationFormComponent } from 'app/pages/consultation-form/consultation-form.component';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(
    component: ConsultationFormComponent): boolean {
      if(component.consultationForm.dirty)
      {
        return confirm('Sigur doriti sa parasiti pagina? Exista modificari ce nu au fost salvate!')
      }
    return true;
  }

}
