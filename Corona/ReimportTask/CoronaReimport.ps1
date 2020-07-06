$parameter = @{
	username = 'corona.app';
	password = '';
	reimport = 'Update';
}

Invoke-WebRequest -Uri https://corona.benediktschmidt.at/api/data/reimport -Method POST -Body $parameter