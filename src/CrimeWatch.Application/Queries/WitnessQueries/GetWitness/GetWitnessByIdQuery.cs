﻿namespace CrimeWatch.Application.Queries.WitnessQueries.GetWitness;
public sealed record GetWitnessByIdQuery(WitnessId WitnessId) : IRequest<Witness>;
