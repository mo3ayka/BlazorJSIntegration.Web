import * as integrationService from './ts/integrationService'

window['blazorIntegration'] = {
    ...window['blazorIntegration'],
    ...integrationService
}