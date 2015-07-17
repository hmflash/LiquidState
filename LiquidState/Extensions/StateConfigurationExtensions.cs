using System;
using System.Diagnostics.Contracts;
using LiquidState.Core;
using LiquidState.Synchronous.Core;

namespace LiquidState
{
    public static class StateConfigurationExtensions
    {
        public static StateConfiguration<TState, TTrigger> OnEntry<TState, TTrigger>(
            this StateConfiguration<TState, TTrigger> config, Action action)
        {
            return config.OnEntry(t => action());
        }

        public static StateConfiguration<TState, TTrigger> OnExit<TState, TTrigger>(
            this StateConfiguration<TState, TTrigger> config, Action action)
        {
            return config.OnExit(t => action());
        }

        public static StateConfiguration<TState, TTrigger> Permit<TState, TTrigger>(
            this StateConfiguration<TState, TTrigger> config, TTrigger trigger, TState resultingState,
            Action onTriggerAction)
        {
            Contract.Requires(trigger != null);
            Contract.Requires(resultingState != null);

            return config.Permit(trigger, resultingState,
                t => onTriggerAction());
        }

        public static StateConfiguration<TState, TTrigger> Permit<TState, TTrigger, TArgument>(this
            StateConfiguration<TState, TTrigger> config, ParameterizedTrigger<TTrigger, TArgument> trigger,
            TState resultingState,
            Action<TArgument> onTriggerAction)
        {
            Contract.Requires(trigger != null);
            Contract.Requires(resultingState != null);

            return config.Permit(trigger, resultingState,
                (t, a) => onTriggerAction
                    (a));
        }

        public static StateConfiguration<TState, TTrigger> PermitIf<TState, TTrigger>(
            StateConfiguration<TState, TTrigger> config, Func<bool> predicate, TTrigger trigger,
            TState resultingState, Action onTriggerAction)
        {
            Contract.Requires(trigger != null);
            Contract.Requires(resultingState != null);

            return config.PermitIf(predicate, trigger, resultingState,
                t => onTriggerAction());
        }

        public static StateConfiguration<TState, TTrigger> PermitIf<TArgument, TState, TTrigger>(
            this StateConfiguration<TState, TTrigger> config, Func<bool> predicate,
            ParameterizedTrigger<TTrigger, TArgument> trigger,
            TState resultingState, Action<TArgument> onTriggerAction)
        {
            Contract.Requires(trigger != null);
            Contract.Requires(resultingState != null);

            return config.PermitIf(predicate, trigger, resultingState,
                (t, a) => onTriggerAction(a));
        }

        public static StateConfiguration<TState, TTrigger> PermitReentry<TState, TTrigger>(
            this StateConfiguration<TState, TTrigger> config, TTrigger trigger, Action onTriggerAction)
        {
            Contract.Requires(trigger != null);
            Contract.Assume(config.CurrentStateRepresentation.State != null);

            return config.PermitReentry(trigger,
                t => onTriggerAction());
        }

        public static StateConfiguration<TState, TTrigger> PermitReentry<TArgument, TState, TTrigger>(
            this StateConfiguration<TState, TTrigger> config,
            ParameterizedTrigger<TTrigger, TArgument> trigger, Action<TArgument> onTriggerAction)
        {
            Contract.Requires(trigger != null);
            Contract.Assume(config.CurrentStateRepresentation.State != null);

            return config.PermitReentry(trigger,
                (t, a) => onTriggerAction(a));
        }

        public static StateConfiguration<TState, TTrigger> PermitReentryIf<TState, TTrigger>(
            this StateConfiguration<TState, TTrigger> config, Func<bool> predicate, TTrigger trigger,
            Action onTriggerAction)
        {
            Contract.Requires(trigger != null);
            Contract.Assume(config.CurrentStateRepresentation.State != null);

            return config.PermitReentryIf(predicate, trigger,
                t => onTriggerAction());
        }

        public static StateConfiguration<TState, TTrigger> PermitReentryIf<TArgument, TState, TTrigger>(
            this StateConfiguration<TState, TTrigger> config, Func<bool> predicate,
            ParameterizedTrigger<TTrigger, TArgument> trigger,
            Action<TArgument> onTriggerAction)
        {
            Contract.Requires(trigger != null);
            Contract.Assume(config.CurrentStateRepresentation.State != null);

            return config.PermitReentryIf(predicate, trigger,
                (t, a) => onTriggerAction(a));
        }

        public static StateConfiguration<TState, TTrigger> PermitDynamic<TState, TTrigger>(
            this StateConfiguration<TState, TTrigger> config, TTrigger trigger,
            Func<DynamicState<TState>> targetStatePredicate,
            Action onTriggerAction)
        {
            Contract.Requires(trigger != null);
            Contract.Requires(targetStatePredicate != null);

            return config.PermitDynamic(trigger, targetStatePredicate,
                t => onTriggerAction());
        }

        public static StateConfiguration<TState, TTrigger> PermitDynamic<TArgument, TState, TTrigger>(
            this StateConfiguration<TState, TTrigger> config,
            ParameterizedTrigger<TTrigger, TArgument> trigger,
            Func<DynamicState<TState>> targetStatePredicate,
            Action<TArgument> onTriggerAction)
        {
            Contract.Requires(trigger != null);
            Contract.Requires(targetStatePredicate != null);

            return config.PermitDynamic(trigger, targetStatePredicate,
                (t, a) => onTriggerAction(a));
        }
    }
}