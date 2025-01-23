export function createIntegrationService(dotNetIntegrationService): IntegrationService {

    return new IntegrationService(dotNetIntegrationService);
}

interface SomeDto {
    count: number,
    name: string;
    lastName: string;
}

class IntegrationService {

    private readonly dotNetIntegrationService;

    constructor(dotNetIntegrationService) {
        this.dotNetIntegrationService = dotNetIntegrationService;
    }

    public async ping(dto: SomeDto): Promise<void> {

        await this.dotNetIntegrationService.invokeMethodAsync('UpdateDto', {
            count: dto.count,
            name: `Nikita and "${dto.name}"`,
            lastName: `Epishev and "${dto.lastName}"`
        });
    }
}