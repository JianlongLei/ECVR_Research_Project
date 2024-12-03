// using System;
// using System.Collections.Generic;
// using Revolve2.Utilities;

// namespace Revolve2.Evolution
// {
//     /// <summary>
//     /// Represents the evolutionary process for modular robots.
//     /// </summary>
//     /// <typeparam name="TPopulation">The type representing the population of modular robots.</typeparam>
//     public class ModularRobotEvolution<TPopulation>
//     {
//         private readonly Selection _parentSelection;
//         private readonly Selector<TPopulation> _survivorSelection;
//         private readonly Evaluator<TPopulation> _evaluator;
//         private readonly Reproducer<TPopulation> _reproducer;
//         private readonly Learner<TPopulation>? _learner;

//         /// <summary>
//         /// Initializes a new instance of the <see cref="ModularRobotEvolution{TPopulation}"/> class.
//         /// </summary>
//         /// <param name="parentSelection">The selector for parent selection.</param>
//         /// <param name="survivorSelection">The selector for survivor selection.</param>
//         /// <param name="evaluator">The evaluator for performance evaluation.</param>
//         /// <param name="reproducer">The reproducer for creating offspring.</param>
//         /// <param name="learner">The learner for additional optimization (optional).</param>
//         public ModularRobotEvolution(
//             Selector<TPopulation> parentSelection,
//             Selector<TPopulation> survivorSelection,
//             Evaluator<TPopulation> evaluator,
//             Reproducer<TPopulation> reproducer,
//             Learner<TPopulation>? learner = null)
//         {
//             _parentSelection = parentSelection ?? throw new ArgumentNullException(nameof(parentSelection));
//             _survivorSelection = survivorSelection ?? throw new ArgumentNullException(nameof(survivorSelection));
//             _evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
//             _reproducer = reproducer ?? throw new ArgumentNullException(nameof(reproducer));
//             _learner = learner;
//         }

//         /// <summary>
//         /// Advances the evolutionary process by one step.
//         /// </summary>
//         /// <param name="population">The current population.</param>
//         /// <param name="kwargs">Additional parameters for the evolutionary step.</param>
//         /// <returns>The population after one evolutionary step.</returns>
//         public TPopulation Step(TPopulation population, Dictionary<string, object>? kwargs = null)
//         {
//             if (population == null) throw new ArgumentNullException(nameof(population));

//             kwargs ??= new Dictionary<string, object>();

//             // Parent selection
//             var (parents, parentKwargs) = _parentSelection.Select(population, kwargs);

//             // Reproduction
//             var children = _reproducer.Reproduce(parents, parentKwargs);

//             // Evaluation of children
//             var childTaskPerformance = _evaluator.Evaluate(children);

//             // Survivor selection
//             var survivors = _survivorSelection.Select(
//                 population,
//                 kwargs,
//                 children,
//                 childTaskPerformance
//             );

//             return survivors;
//         }
//     }
// }
