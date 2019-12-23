$resourceGroup = 'Googazon'
$namespace = 'Googazon-Rivers'

$topic = 'customerservice'

$subscription = 'callcenter'
New-AzureRmServiceBusSubscription -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Name $subscription
Remove-AzureRmServiceBusRule -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Subscription $subscription -Name '$Default' -Force
New-AzureRmServiceBusRule -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Subscription $subscription -Name Default -SqlExpression "need='contact'"

$subscription = 'chat'
New-AzureRmServiceBusSubscription -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Name $subscription
Remove-AzureRmServiceBusRule -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Subscription $subscription -Name '$Default' -Force
New-AzureRmServiceBusRule -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Subscription $subscription -Name Default -SqlExpression "need='contact'"

$subscription = 'email'
New-AzureRmServiceBusSubscription -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Name $subscription
Remove-AzureRmServiceBusRule -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Subscription $subscription -Name '$Default' -Force
New-AzureRmServiceBusRule -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Subscription $subscription -Name Default -SqlExpression "need='contact'"

$subscription = 'brickandmortar'
New-AzureRmServiceBusSubscription -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Name $subscription
Remove-AzureRmServiceBusRule -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Subscription $subscription -Name '$Default' -Force
New-AzureRmServiceBusRule -ResourceGroupName $resourceGroup -Namespace $namespace -Topic $topic -Subscription $subscription -Name Default -SqlExpression "need='contact'"
