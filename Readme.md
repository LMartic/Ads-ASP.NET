Poenta aplikacije da korisnici kace svoje oglase, kako bi nesto prodali.
Administrator moze da obrise svaki oglas kao i da menja, dok obicni korisnici mogu samo svoje oglase da brisu i menjaju.
Sva validacija je na serveru, s toga bilo kakav pokusaj zloupotrebe nece se odraziti na bazu.
Prilikom kreiranja baze kreiraju se korisnic:
admin@domain.com
password

member@domain.com
password

Da bi aplikacije radila u potpunosti ispravno potrebmo je da u projektu Ads.API Ads.MVC u fajlu appsettings.json unesu podesavanja za email
kako bi aplikacija mogla da salje mailove kada je kreiran ogals
