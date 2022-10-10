import { AzureNamedKeyCredential, TableClient } from "@azure/data-tables";
import { AzureFunction, Context, HttpRequest } from "@azure/functions"

const httpTrigger: AzureFunction = async function (context: Context, req: HttpRequest): Promise<void> {
    context.log("[sensorData] Starting function");
    
    const content = (req.body && req.body) || "";
    context.log(`[sensorData] Completed reading request body, length ${content.length}`);

    const accountName = process.env.STORAGE_ACCOUNT_NAME;
    const accountKey = process.env.STORAGE_ACCOUNT_KEY;
    const tableName = process.env.STORAGE_ACCOUNT_TABLE_NAME;
    context.log(`[sensorData] Creating client for table ${tableName} in account ${accountName}`);

    const credential = new AzureNamedKeyCredential(accountName, accountKey);
    const tableClient = new TableClient(`https://${accountName}.table.core.windows.net`, tableName, credential);

    const now = new Date();
    const newEntity = {
        partitionKey: "12345",
        rowKey: now.toISOString(),
        json: content
    };

    await tableClient.createEntity(newEntity);
};

export default httpTrigger;