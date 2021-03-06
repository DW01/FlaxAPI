// Flax Engine scripting API

namespace FlaxEngine.Utilities
{
    /// <summary>
    /// State machine state
    /// </summary>
    public abstract class State
    {
        internal StateMachine owner;

        /// <summary>
        /// Gets the state machine.
        /// </summary>
        /// <value>
        /// The state machine.
        /// </value>
        public StateMachine StateMachine => owner;

        /// <summary>
        /// Gets a value indicating whether this state is active.
        /// </summary>
        /// <value><c>true</c> if this state is active; otherwise, <c>false</c>.</value>
        public bool IsActive => owner != null && owner.CurrentState == this;

        /// <summary>
        /// Checks if can enter to that state
        /// </summary>
        /// <returns>True if can enter to that state, otherwise false</returns>
        public virtual bool CanEnter()
        {
            return true;
        }

        /// <summary>
        /// Checks if can exit from that state
        /// </summary>
        /// <param name="nextState">Next state to ener after exit from the current state</param>
        /// <returns>True if can exit from that state, otherwise false</returns>
        public virtual bool CanExit(State nextState)
        {
            return true;
        }

        /// <summary>
        /// Called when state is starting to be active.
        /// </summary>
        public virtual void OnEnter()
        {
        }

        /// <summary>
        /// Called when state is ending to be active.
        /// </summary>
        /// <param name="nextState">The next state.</param>
        public virtual void OnExit(State nextState)
        {
        }
    }
}
